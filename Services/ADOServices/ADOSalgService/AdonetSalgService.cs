using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using Microsoft.Data.Sqlite;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOSalgService
{
    public class AdonetSalgService
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public AdonetSalgService() { }

        public AdonetSalgService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Salg> GetAllSalgs()
        {
            var salgList = new List<Salg>();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            const string sql = @"
SELECT 
    Salg.Salg_ID,
    Born.Born_ID,
    Born.Navn,
    Born.Telefon,
    Bornegruppe.Gruppenavn,
    Leder.Navn,
    Salg.Dato,
    Salg.AntalLodseddelerRetur,
    Salg.AntalSolgteLodseddelerPrSalg,
    Born.AntalSolgteLodseddeler,
    Salg.Pris
FROM Salg
LEFT JOIN Bornegruppe ON Bornegruppe.Bornegruppe_ID = Salg.Bornegruppe_ID
LEFT JOIN Born        ON Born.Born_ID = Salg.Born_ID
LEFT JOIN Leder       ON Leder.Leder_ID = Salg.Leder_ID";

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var salg = new Salg
                {
                    Leder = new Leder(),
                    Born = new Born(),
                    Bornegruppe = new Bornegruppe(),

                    Salg_ID = reader.GetInt32(0),
                    Born_ID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                    Dato = reader.GetDateTime(6),
                    AntalLodseddelerRetur = reader.GetInt32(7),
                    AntalSolgteLodseddelerPrSalg = reader.GetInt32(8),
                    Pris = reader.GetDouble(10)
                };

                salg.Born.Navn = reader.IsDBNull(2) ? "(ukendt barn)" : reader.GetString(2);
                salg.Born.Telefon = reader.IsDBNull(3) ? "" : reader.GetString(3);
                salg.Bornegruppe.Gruppenavn = reader.IsDBNull(4) ? "(ukendt gruppe)" : reader.GetString(4);
                salg.Leder.Navn = reader.IsDBNull(5) ? "(ukendt leder)" : reader.GetString(5);
                salg.Born.AntalSolgteLodseddeler = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);

                salgList.Add(salg);
            }


            return salgList;
        }

        public Salg GetSalgById(int id)
        {
            var salg = new Salg();
            const string sql = "SELECT * FROM Salg WHERE Salg_ID = @Salg_ID";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Salg_ID", id);

            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                salg.Salg_ID = Convert.ToInt32(reader["Salg_ID"]);
                salg.Born_ID = Convert.ToInt32(reader["Born_ID"]);
                salg.Bornegruppe_ID = Convert.ToInt32(reader["Bornegruppe_ID"]);
                salg.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                salg.Dato = Convert.ToDateTime(reader["Dato"]);
                salg.AntalLodseddelerRetur = Convert.ToInt32(reader["AntalLodseddelerRetur"]);
                salg.AntalSolgteLodseddelerPrSalg = Convert.ToInt32(reader["AntalSolgteLodseddelerPrSalg"]);
                salg.Pris = Convert.ToDouble(reader["Pris"]);
            }

            return salg;
        }

        public Salg CreateSalg(Salg salg)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Tildel ID hvis mangler
                if (salg.Salg_ID <= 0)
                {
                    using var getId = new SqliteCommand("SELECT COALESCE(MAX(Salg_ID),0)+1 FROM Salg;", connection, transaction);
                    salg.Salg_ID = Convert.ToInt32(getId.ExecuteScalar());
                }

                const string insertSql = @"
INSERT INTO Salg (Salg_ID, Born_ID, Bornegruppe_ID, Leder_ID, Dato, AntalLodseddelerRetur, AntalSolgteLodseddelerPrSalg, Pris)
VALUES (@Salg_ID, @Born_ID, @Bornegruppe_ID, @Leder_ID, @Dato, @AntalLodseddelerRetur, @AntalSolgteLodseddelerPrSalg, @Pris);";

                using (var insertCommand = new SqliteCommand(insertSql, connection, transaction))
                {
                    insertCommand.Parameters.AddWithValue("@Salg_ID", salg.Salg_ID);
                    insertCommand.Parameters.AddWithValue("@Born_ID", salg.Born_ID);
                    insertCommand.Parameters.AddWithValue("@Bornegruppe_ID", salg.Bornegruppe_ID);
                    insertCommand.Parameters.AddWithValue("@Leder_ID", salg.Leder_ID);
                    insertCommand.Parameters.AddWithValue("@Dato", salg.Dato);
                    insertCommand.Parameters.AddWithValue("@AntalLodseddelerRetur", salg.AntalLodseddelerRetur);
                    insertCommand.Parameters.AddWithValue("@AntalSolgteLodseddelerPrSalg", salg.AntalSolgteLodseddelerPrSalg);
                    insertCommand.Parameters.AddWithValue("@Pris", salg.Pris);

                    insertCommand.ExecuteNonQuery();
                }

                // Opdater aggregerede felter
                if (salg.AntalSolgteLodseddelerPrSalg != 0)
                {
                    const string sqlBorn1 =
                        "UPDATE Born SET AntalSolgteLodseddeler = AntalSolgteLodseddeler + @Antal WHERE Born_ID = @Born_ID";
                    using (var cmd = new SqliteCommand(sqlBorn1, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Born_ID", salg.Born_ID);
                        cmd.Parameters.AddWithValue("@Antal", salg.AntalSolgteLodseddelerPrSalg);
                        cmd.ExecuteNonQuery();
                    }

                    const string sqlBorn2 =
                        "UPDATE Born SET GivetLodsedler = GivetLodsedler - @Antal WHERE Born_ID = @Born_ID";
                    using (var cmd = new SqliteCommand(sqlBorn2, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Born_ID", salg.Born_ID);
                        cmd.Parameters.AddWithValue("@Antal", salg.AntalSolgteLodseddelerPrSalg);
                        cmd.ExecuteNonQuery();
                    }

                    const string sqlGruppe =
                        "UPDATE Bornegruppe SET AntalSolgteLodSeddelerPrGruppe = AntalSolgteLodSeddelerPrGruppe + @Antal WHERE Bornegruppe_ID = @Bornegruppe_ID";
                    using (var cmd = new SqliteCommand(sqlGruppe, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Bornegruppe_ID", salg.Bornegruppe_ID);
                        cmd.Parameters.AddWithValue("@Antal", salg.AntalSolgteLodseddelerPrSalg);
                        cmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

            return salg;
        }

        public Salg DeleteSalg(Salg salg)
        {
            const string sql = "DELETE FROM Salg WHERE Salg_ID = @Salg_ID";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);

            connection.Open();
            command.Parameters.AddWithValue("@Salg_ID", salg.Salg_ID);
            command.ExecuteNonQuery();

            return salg;
        }

        public Salg UpdateSalg(Salg salg)
        {
            const string sql = @"
UPDATE Salg 
SET Born_ID = @Born_ID,
    Bornegruppe_ID = @Bornegruppe_ID,
    Leder_ID = @Leder_ID,
    Dato = @Dato,
    AntalLodseddelerRetur = @AntalLodseddelerRetur,
    AntalSolgteLodseddelerPrSalg = @AntalSolgteLodseddelerPrSalg,
    Pris = @Pris
WHERE Salg_ID = @Salg_ID";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);

            connection.Open();

            command.Parameters.AddWithValue("@Salg_ID", salg.Salg_ID);
            command.Parameters.AddWithValue("@Born_ID", salg.Born_ID);
            command.Parameters.AddWithValue("@Bornegruppe_ID", salg.Bornegruppe_ID);
            command.Parameters.AddWithValue("@Leder_ID", salg.Leder_ID);
            command.Parameters.AddWithValue("@Dato", salg.Dato);
            command.Parameters.AddWithValue("@AntalLodseddelerRetur", salg.AntalLodseddelerRetur);
            command.Parameters.AddWithValue("@AntalSolgteLodseddelerPrSalg", salg.AntalSolgteLodseddelerPrSalg);
            command.Parameters.AddWithValue("@Pris", salg.Pris);

            command.ExecuteNonQuery();
            return salg;
        }

        public List<Salg> GetBornegruppeByID(int ID)
        {
            var listsalg = new List<Salg>();
            const string sql = "SELECT * FROM Salg WHERE Bornegruppe_ID = @Bornegruppe_ID";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Bornegruppe_ID", ID);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var salg = new Salg
                {
                    Salg_ID = Convert.ToInt32(reader["Salg_ID"]),
                    Born_ID = Convert.ToInt32(reader["Born_ID"]),
                    Bornegruppe_ID = Convert.ToInt32(reader["Bornegruppe_ID"]),
                    Leder_ID = Convert.ToInt32(reader["Leder_ID"]),
                    Dato = Convert.ToDateTime(reader["Dato"]),
                    AntalLodseddelerRetur = Convert.ToInt32(reader["AntalLodseddelerRetur"]),
                    AntalSolgteLodseddelerPrSalg = Convert.ToInt32(reader["AntalSolgteLodseddelerPrSalg"]),
                    Pris = Convert.ToDouble(reader["Pris"])
                };
                listsalg.Add(salg);
            }

            return listsalg;
        }

        public IEnumerable<Salg> PriceFilters(float maxPrice, float minPrice)
        {
            if (minPrice < 0 || maxPrice < 0)
                throw new NegativeAmountExceptioncs("MinPrice and MaxPrice cannot be negative values.");

            var filterList = new List<Salg>();

            const string sql = @"
SELECT * FROM Salg 
WHERE (@MinPrice = 0 OR Pris >= @MinPrice) 
  AND (@MaxPrice = 0 OR Pris <= @MaxPrice)";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@MinPrice", minPrice);
            command.Parameters.AddWithValue("@MaxPrice", maxPrice);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var salg = new Salg
                {
                    Salg_ID = Convert.ToInt32(reader["Salg_ID"]),
                    Born_ID = Convert.ToInt32(reader["Born_ID"]),
                    Bornegruppe_ID = Convert.ToInt32(reader["Bornegruppe_ID"]),
                    Leder_ID = Convert.ToInt32(reader["Leder_ID"]),
                    Dato = Convert.ToDateTime(reader["Dato"]),
                    AntalLodseddelerRetur = Convert.ToInt32(reader["AntalLodseddelerRetur"]),
                    AntalSolgteLodseddelerPrSalg = Convert.ToInt32(reader["AntalSolgteLodseddelerPrSalg"]),
                    Pris = Convert.ToDouble(reader["Pris"])
                };

                filterList.Add(salg);
            }

            return filterList;
        }

        public IEnumerable<Salg> GetBornById(int id, int bid)
        {
            var listsalg = new List<Salg>();
            const string sql = "SELECT * FROM Born WHERE Born_ID = @Born_ID AND Bornegruppe_ID = @Bornegruppe_ID";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Born_ID", id);
            command.Parameters.AddWithValue("@Bornegruppe_ID", bid);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var salg = new Salg
                {
                    Born_ID = Convert.ToInt32(reader["Born_ID"]),
                    Bornegruppe_ID = Convert.ToInt32(reader["Bornegruppe_ID"])
                };
                listsalg.Add(salg);
            }

            return listsalg;
        }

        public List<Leder> GetLederOptions()
        {
            var lederOptions = new List<Leder>();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            const string sql = "SELECT Leder_ID, Navn FROM Leder ORDER BY Navn";
            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var leder = new Leder
                {
                    Leder_ID = reader.GetInt32(0),
                    Navn = reader.GetString(1)
                };
                lederOptions.Add(leder);
            }

            return lederOptions;
        }

        public List<Salg> GetAntalSolgteLodseddelerDESC()
        {
            return GetAllOrderBy("Born.AntalSolgteLodseddeler DESC");
        }

        public List<Salg> GetAntalSolgteLodseddelerASC()
        {
            return GetAllOrderBy("Born.AntalSolgteLodseddeler ASC");
        }

        // helper
        private List<Salg> GetAllOrderBy(string orderBy)
        {
            var list = new List<Salg>();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var sql = $@"
SELECT 
    Salg.Salg_ID,
    Born.Born_ID,
    Born.Navn,
    Born.Telefon,
    Bornegruppe.Gruppenavn,
    Leder.Navn,
    Salg.Dato,
    Salg.AntalLodseddelerRetur,
    Salg.AntalSolgteLodseddelerPrSalg,
    Born.AntalSolgteLodseddeler,
    Salg.Pris
FROM Salg
JOIN Bornegruppe ON Bornegruppe.Bornegruppe_ID = Salg.Bornegruppe_ID
JOIN Born        ON Born.Born_ID = Salg.Born_ID
JOIN Leder       ON Leder.Leder_ID = Salg.Leder_ID
ORDER BY {orderBy}";

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var salg = new Salg
                {
                    Leder = new Leder(),
                    Born = new Born(),
                    Bornegruppe = new Bornegruppe(),

                    Salg_ID = reader.GetInt32(0),
                    Born_ID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                    Dato = reader.GetDateTime(6),
                    AntalLodseddelerRetur = reader.GetInt32(7),
                    AntalSolgteLodseddelerPrSalg = reader.GetInt32(8),
                    Pris = reader.GetDouble(10)
                };

                salg.Born.Navn = reader.GetString(2);
                salg.Born.Telefon = reader.GetString(3);
                salg.Bornegruppe.Gruppenavn = reader.GetString(4);
                salg.Leder.Navn = reader.GetString(5);
                salg.Born.AntalSolgteLodseddeler = reader.GetInt32(9);

                list.Add(salg);
            }

            return list;
        }
    }
}
