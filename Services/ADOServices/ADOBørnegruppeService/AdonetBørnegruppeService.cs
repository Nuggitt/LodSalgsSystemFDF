using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using Microsoft.Data.Sqlite;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService
{
    public class AdonetBørnegruppeService
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public AdonetBørnegruppeService() { }

        public AdonetBørnegruppeService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Børnegruppe>> GetAllBørnegruppeAsync()
        {
            var lstbørnegruppe = new List<Børnegruppe>();
            const string sql = "SELECT * FROM Børnegruppe";

            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();
                using var command = new SqliteCommand(sql, connection);
                using var dataReader = await command.ExecuteReaderAsync();

                while (await dataReader.ReadAsync())
                {
                    var børnegruppe = new Børnegruppe
                    {
                        Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]),
                        Gruppenavn = Convert.ToString(dataReader["Gruppenavn"])!,
                        Lokale = Convert.ToString(dataReader["Lokale"])!,
                        Antalbørn = Convert.ToInt32(dataReader["Antalbørn"]),
                        Leder_ID = Convert.ToInt32(dataReader["Leder_ID"]),
                        AntalLodSeddelerPrGruppe = Convert.ToInt32(dataReader["AntalLodSeddelerPrGruppe"]),
                        AntalSolgteLodseddelerPrGruppe = Convert.ToInt32(dataReader["AntalSolgteLodSeddelerPrGruppe"])
                    };

                    lstbørnegruppe.Add(børnegruppe);
                }
            }
            return lstbørnegruppe;
        }

        public async Task<Børnegruppe> GetBørnegruppeById(int id)
        {
            var børnegruppe = new Børnegruppe();
            const string sql = "SELECT * FROM Børnegruppe WHERE Børnegruppe_ID = @Børnegruppe_ID";

            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();
                using var command = new SqliteCommand(sql, connection);
                command.Parameters.AddWithValue("@Børnegruppe_ID", id);

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    børnegruppe.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);
                    børnegruppe.Gruppenavn = Convert.ToString(reader["Gruppenavn"])!;
                    børnegruppe.Lokale = Convert.ToString(reader["Lokale"])!;
                    børnegruppe.Antalbørn = Convert.ToInt32(reader["Antalbørn"]);
                    børnegruppe.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                    børnegruppe.AntalLodSeddelerPrGruppe = Convert.ToInt32(reader["AntalLodSeddelerPrGruppe"]);
                    børnegruppe.AntalSolgteLodseddelerPrGruppe = Convert.ToInt32(reader["AntalSolgteLodSeddelerPrGruppe"]);
                }
            }
            return børnegruppe;
        }

        public Børnegruppe CreateBørnegruppe(Børnegruppe børnegruppe)
        {
            if (børnegruppe.AntalLodSeddelerPrGruppe < 0 ||
                børnegruppe.Antalbørn < 0 ||
                børnegruppe.AntalSolgteLodseddelerPrGruppe < 0)
            {
                throw new NegativeAmountExceptioncs("Værdi må ikke være negativt");
            }

            const string sql = @"
INSERT INTO Børnegruppe (Gruppenavn, Lokale, Antalbørn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe)
VALUES (@Gruppenavn, @Lokale, @Antalbørn, @Leder_ID, @AntalLodSeddelerPrGruppe, @AntalSolgteLodSeddelerPrGruppe);
SELECT last_insert_rowid();";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Gruppenavn", børnegruppe.Gruppenavn);
            command.Parameters.AddWithValue("@Lokale", børnegruppe.Lokale);
            command.Parameters.AddWithValue("@Antalbørn", børnegruppe.Antalbørn);
            command.Parameters.AddWithValue("@Leder_ID", børnegruppe.Leder_ID);
            command.Parameters.AddWithValue("@AntalLodSeddelerPrGruppe", børnegruppe.AntalLodSeddelerPrGruppe);
            command.Parameters.AddWithValue("@AntalSolgteLodSeddelerPrGruppe", børnegruppe.AntalSolgteLodseddelerPrGruppe);

            var newId = (long)command.ExecuteScalar()!;
            børnegruppe.Børnegruppe_ID = (int)newId;

            return børnegruppe;
        }


        public bool TjekIdEksisterer(string børnegruppeId)
        {
            const string sql = "SELECT COUNT(*) FROM Børnegruppe WHERE Børnegruppe_ID = @Børnegruppe_ID";

            using (var connection = new SqliteConnection(connectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Børnegruppe_ID", børnegruppeId);
                var count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        public Børnegruppe DeleteBørnegruppe(Børnegruppe børnegruppe)
        {
            const string sql = "DELETE FROM Børnegruppe WHERE Børnegruppe_ID = @Børnegruppe_ID";

            using (var connection = new SqliteConnection(connectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Børnegruppe_ID", børnegruppe.Børnegruppe_ID);
                command.ExecuteNonQuery();
            }
            return børnegruppe;
        }

        public Børnegruppe UpdateBørnegruppe(Børnegruppe børnegruppe)
        {
            const string sql = @"
UPDATE Børnegruppe
SET
  Gruppenavn = @Gruppenavn,
  Lokale = @Lokale,
  Antalbørn = @Antalbørn,
  Leder_ID = @Leder_ID,                                   
  AntalLodSeddelerPrGruppe = @AntalLodSeddelerPrGruppe,
  AntalSolgteLodSeddelerPrGruppe = @AntalSolgteLodSeddelerPrGruppe
WHERE Børnegruppe_ID = @Børnegruppe_ID;";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Børnegruppe_ID", børnegruppe.Børnegruppe_ID);
            command.Parameters.AddWithValue("@Gruppenavn", børnegruppe.Gruppenavn);
            command.Parameters.AddWithValue("@Lokale", børnegruppe.Lokale);
            command.Parameters.AddWithValue("@Antalbørn", børnegruppe.Antalbørn);
            command.Parameters.AddWithValue("@Leder_ID", børnegruppe.Leder_ID);                         // <== ny
            command.Parameters.AddWithValue("@AntalLodSeddelerPrGruppe", børnegruppe.AntalLodSeddelerPrGruppe);
            command.Parameters.AddWithValue("@AntalSolgteLodSeddelerPrGruppe", børnegruppe.AntalSolgteLodseddelerPrGruppe);

            var rows = command.ExecuteNonQuery();
            if (rows == 0)
                throw new InvalidOperationException($"Ingen række opdateret for Børnegruppe_ID={børnegruppe.Børnegruppe_ID}");

            return børnegruppe;
        }


        public List<Børnegruppe> GetBørnegruppeByName(string name)
        {
            var lstbørnegruppe = new List<Børnegruppe>();
            const string sql = "SELECT * FROM Børnegruppe WHERE Gruppenavn LIKE @Gruppenavn";

            using (var connection = new SqliteConnection(connectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Gruppenavn", name); // beholdt som din kode

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var børnegruppe = new Børnegruppe
                    {
                        Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]),
                        Gruppenavn = Convert.ToString(reader["Gruppenavn"])!,
                        Lokale = Convert.ToString(reader["Lokale"])!,
                        Antalbørn = Convert.ToInt32(reader["Antalbørn"]),
                        Leder_ID = Convert.ToInt32(reader["Leder_ID"]),
                        AntalLodSeddelerPrGruppe = Convert.ToInt32(reader["AntalLodSeddelerPrGruppe"]),
                        AntalSolgteLodseddelerPrGruppe = Convert.ToInt32(reader["AntalSolgteLodSeddelerPrGruppe"])
                    };
                    lstbørnegruppe.Add(børnegruppe);
                }
            }
            return lstbørnegruppe;
        }

        public List<Børnegruppe> GetAllBørnGruppeIDDescending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY Børnegruppe_ID DESC");
        }

        public List<Børnegruppe> GetAllBørnGruppeIDAscending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY Børnegruppe_ID ASC");
        }

        public List<Børnegruppe> SortAllGruppeNavnDescending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY Gruppenavn DESC");
        }

        public List<Børnegruppe> SortAllGruppeNavnAscending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY Gruppenavn ASC");
        }

        public List<Børnegruppe> SortAllAntalBørnDescending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY Antalbørn DESC");
        }

        public List<Børnegruppe> SortAllAntalBørnAscending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY Antalbørn ASC");
        }

        public List<Børnegruppe> SortAllLederIDDescending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY Leder_ID DESC");
        }

        public List<Børnegruppe> SortAllLederIDAscending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY Leder_ID ASC");
        }

        public List<Børnegruppe> SortAllAntalLodSeddelerPrGruppeDescending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY AntalLodSeddelerPrGruppe DESC");
        }

        public List<Børnegruppe> SortAllAntalLodSeddelerPrGruppeAscending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY AntalLodSeddelerPrGruppe ASC");
        }

        public List<Børnegruppe> SortAllAntalSolgtePrGruppeDescending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY AntalSolgteLodSeddelerPrGruppe DESC");
        }

        public List<Børnegruppe> SortAllAntalSolgtePrGruppeAscending()
        {
            return QueryMany("SELECT * FROM Børnegruppe ORDER BY AntalSolgteLodSeddelerPrGruppe ASC");
        }

        public Børnegruppe TildelLodsedlerBørnegruppe(Børnegruppe børngruppe, int amount)
        {
            const string sql = "UPDATE Børnegruppe SET AntalLodSeddelerPrGruppe = AntalLodSeddelerPrGruppe + @AntalLodSeddelerPrGruppe WHERE Børnegruppe_ID = @Børnegruppe_ID";

            using (var connection = new SqliteConnection(connectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Børnegruppe_ID", børngruppe.Børnegruppe_ID);
                command.Parameters.AddWithValue("@AntalLodSeddelerPrGruppe", amount);
                command.ExecuteNonQuery();
            }
            return børngruppe;
        }

        public List<Leder> GetLederOptions()
        {
            var lederOptions = new List<Leder>();
            const string sql = "SELECT Leder_ID, Navn FROM Leder ORDER BY Navn";

            using (var connection = new SqliteConnection(connectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();
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
            }
            return lederOptions;
        }

        // Helper til at reducere gentagelser
        private List<Børnegruppe> QueryMany(string sql)
        {
            var list = new List<Børnegruppe>();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            using var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                var b = new Børnegruppe
                {
                    Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]),
                    Gruppenavn = Convert.ToString(dataReader["Gruppenavn"])!,
                    Lokale = Convert.ToString(dataReader["Lokale"])!,
                    Antalbørn = Convert.ToInt32(dataReader["Antalbørn"]),
                    Leder_ID = Convert.ToInt32(dataReader["Leder_ID"]),
                    AntalLodSeddelerPrGruppe = Convert.ToInt32(dataReader["AntalLodSeddelerPrGruppe"]),
                    AntalSolgteLodseddelerPrGruppe = Convert.ToInt32(dataReader["AntalSolgteLodSeddelerPrGruppe"])
                };
                list.Add(b);
            }
            return list;
        }
    }
}

