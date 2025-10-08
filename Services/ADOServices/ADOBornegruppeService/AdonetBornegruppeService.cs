using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using Microsoft.Data.Sqlite;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBornegruppeService
{
    public class AdonetBornegruppeService
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public AdonetBornegruppeService() { }

        public AdonetBornegruppeService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Bornegruppe>> GetAllBornegruppeAsync()
        {
            var lstbornegruppe = new List<Bornegruppe>();
            const string sql = "SELECT * FROM Bornegruppe";

            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();
                using var command = new SqliteCommand(sql, connection);
                using var dataReader = await command.ExecuteReaderAsync();

                while (await dataReader.ReadAsync())
                {
                    var bornegruppe = new Bornegruppe
                    {
                        Bornegruppe_ID = Convert.ToInt32(dataReader["Bornegruppe_ID"]),
                        Gruppenavn = Convert.ToString(dataReader["Gruppenavn"])!,
                        Lokale = Convert.ToString(dataReader["Lokale"])!,
                        Antalborn = Convert.ToInt32(dataReader["Antalborn"]),
                        Leder_ID = Convert.ToInt32(dataReader["Leder_ID"]),
                        AntalLodSeddelerPrGruppe = Convert.ToInt32(dataReader["AntalLodSeddelerPrGruppe"]),
                        AntalSolgteLodseddelerPrGruppe = Convert.ToInt32(dataReader["AntalSolgteLodSeddelerPrGruppe"])
                    };

                    lstbornegruppe.Add(bornegruppe);
                }
            }
            return lstbornegruppe;
        }

        public async Task<Bornegruppe> GetBornegruppeById(int id)
        {
            var bornegruppe = new Bornegruppe();
            const string sql = "SELECT * FROM Bornegruppe WHERE Bornegruppe_ID = @Bornegruppe_ID";

            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();
                using var command = new SqliteCommand(sql, connection);
                command.Parameters.AddWithValue("@Bornegruppe_ID", id);

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    bornegruppe.Bornegruppe_ID = Convert.ToInt32(reader["Bornegruppe_ID"]);
                    bornegruppe.Gruppenavn = Convert.ToString(reader["Gruppenavn"])!;
                    bornegruppe.Lokale = Convert.ToString(reader["Lokale"])!;
                    bornegruppe.Antalborn = Convert.ToInt32(reader["Antalborn"]);
                    bornegruppe.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                    bornegruppe.AntalLodSeddelerPrGruppe = Convert.ToInt32(reader["AntalLodSeddelerPrGruppe"]);
                    bornegruppe .AntalSolgteLodseddelerPrGruppe = Convert.ToInt32(reader["AntalSolgteLodSeddelerPrGruppe"]);
                }
            }
            return bornegruppe;
        }

        public Bornegruppe CreateBornegruppe(Bornegruppe bornegruppe)
        {
            if (bornegruppe.AntalLodSeddelerPrGruppe < 0 ||
                bornegruppe.Antalborn < 0 ||
                bornegruppe.AntalSolgteLodseddelerPrGruppe < 0)
            {
                throw new NegativeAmountExceptioncs("Værdi må ikke være negativt");
            }

            const string sql = @"
INSERT INTO Bornegruppe (Gruppenavn, Lokale, Antalborn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe)
VALUES (@Gruppenavn, @Lokale, @Antalborn, @Leder_ID, @AntalLodSeddelerPrGruppe, @AntalSolgteLodSeddelerPrGruppe);
SELECT last_insert_rowid();";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Gruppenavn", bornegruppe.Gruppenavn);
            command.Parameters.AddWithValue("@Lokale", bornegruppe.Lokale);
            command.Parameters.AddWithValue("@Antalborn", bornegruppe.Antalborn);
            command.Parameters.AddWithValue("@Leder_ID", bornegruppe.Leder_ID);
            command.Parameters.AddWithValue("@AntalLodSeddelerPrGruppe", bornegruppe.AntalLodSeddelerPrGruppe);
            command.Parameters.AddWithValue("@AntalSolgteLodSeddelerPrGruppe", bornegruppe.AntalSolgteLodseddelerPrGruppe);

            var newId = (long)command.ExecuteScalar()!;
            bornegruppe.Bornegruppe_ID = (int)newId;

            return bornegruppe;
        }


        public bool TjekIdEksisterer(string bornegruppeId)
        {
            const string sql = "SELECT COUNT(*) FROM Bornegruppe WHERE Bornegruppe_ID = @Bornegruppe_ID";

            using (var connection = new SqliteConnection(connectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Bornegruppe_ID", bornegruppeId);
                var count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        public Bornegruppe DeleteBornegruppe(Bornegruppe bornegruppe)
        {
            const string sql = "DELETE FROM Bornegruppe WHERE Bornegruppe_ID = @Bornegruppe_ID";

            using (var connection = new SqliteConnection(connectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Bornegruppe_ID", bornegruppe.Bornegruppe_ID);
                command.ExecuteNonQuery();
            }
            return bornegruppe;
        }

        public Bornegruppe UpdateBornegruppe(Bornegruppe bornegruppe)
        {
            const string sql = @"
UPDATE Bornegruppe
SET
  Gruppenavn = @Gruppenavn,
  Lokale = @Lokale,
  Antalborn = @Antalborn,
  Leder_ID = @Leder_ID,                                   
  AntalLodSeddelerPrGruppe = @AntalLodSeddelerPrGruppe,
  AntalSolgteLodSeddelerPrGruppe = @AntalSolgteLodSeddelerPrGruppe
WHERE Bornegruppe_ID = @Bornegruppe_ID;";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Bornegruppe_ID", bornegruppe.Bornegruppe_ID);
            command.Parameters.AddWithValue("@Gruppenavn", bornegruppe.Gruppenavn);
            command.Parameters.AddWithValue("@Lokale", bornegruppe.Lokale);
            command.Parameters.AddWithValue("@Antalborn", bornegruppe.Antalborn);
            command.Parameters.AddWithValue("@Leder_ID", bornegruppe.Leder_ID);                         // <== ny
            command.Parameters.AddWithValue("@AntalLodSeddelerPrGruppe", bornegruppe.AntalLodSeddelerPrGruppe);
            command.Parameters.AddWithValue("@AntalSolgteLodSeddelerPrGruppe", bornegruppe.AntalSolgteLodseddelerPrGruppe);

            var rows = command.ExecuteNonQuery();
            if (rows == 0)
                throw new InvalidOperationException($"Ingen række opdateret for Bornegruppe_ID={bornegruppe.Bornegruppe_ID}");

            return bornegruppe;
        }


        public List<Bornegruppe> GetBornegruppeByName(string name)
        {
            var lstbornegruppe = new List<Bornegruppe>();
            const string sql = "SELECT * FROM Bornegruppe WHERE Gruppenavn LIKE @Gruppenavn";

            using (var connection = new SqliteConnection(connectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Gruppenavn", name); // beholdt som din kode

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var bornegruppe = new Bornegruppe
                    {
                        Bornegruppe_ID = Convert.ToInt32(reader["Bornegruppe_ID"]),
                        Gruppenavn = Convert.ToString(reader["Gruppenavn"])!,
                        Lokale = Convert.ToString(reader["Lokale"])!,
                        Antalborn = Convert.ToInt32(reader["Antalborn"]),
                        Leder_ID = Convert.ToInt32(reader["Leder_ID"]),
                        AntalLodSeddelerPrGruppe = Convert.ToInt32(reader["AntalLodSeddelerPrGruppe"]),
                        AntalSolgteLodseddelerPrGruppe = Convert.ToInt32(reader["AntalSolgteLodSeddelerPrGruppe"])
                    };
                    lstbornegruppe.Add(bornegruppe);
                }
            }
            return lstbornegruppe;
        }

        public List<Bornegruppe> GetAllBornGruppeIDDescending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY Bornegruppe_ID DESC");
        }

        public List<Bornegruppe> GetAllBornGruppeIDAscending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY Bornegruppe_ID ASC");
        }

        public List<Bornegruppe> SortAllGruppeNavnDescending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY Gruppenavn DESC");
        }

        public List<Bornegruppe> SortAllGruppeNavnAscending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY Gruppenavn ASC");
        }

        public List<Bornegruppe> SortAllAntalBornDescending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY Antalborn DESC");
        }

        public List<Bornegruppe> SortAllAntalBornAscending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY Antalborn ASC");
        }

        public List<Bornegruppe> SortAllLederIDDescending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY Leder_ID DESC");
        }

        public List<Bornegruppe> SortAllLederIDAscending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY Leder_ID ASC");
        }

        public List<Bornegruppe> SortAllAntalLodSeddelerPrGruppeDescending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY AntalLodSeddelerPrGruppe DESC");
        }

        public List<Bornegruppe> SortAllAntalLodSeddelerPrGruppeAscending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY AntalLodSeddelerPrGruppe ASC");
        }

        public List<Bornegruppe> SortAllAntalSolgtePrGruppeDescending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY AntalSolgteLodSeddelerPrGruppe DESC");
        }

        public List<Bornegruppe> SortAllAntalSolgtePrGruppeAscending()
        {
            return QueryMany("SELECT * FROM Bornegruppe ORDER BY AntalSolgteLodSeddelerPrGruppe ASC");
        }

        public Bornegruppe TildelLodsedlerBornegruppe(Bornegruppe borngruppe, int amount)
        {
            const string sql = "UPDATE Bornegruppe SET AntalLodSeddelerPrGruppe = AntalLodSeddelerPrGruppe + @AntalLodSeddelerPrGruppe WHERE Bornegruppe_ID = @Bornegruppe_ID";

            using (var connection = new SqliteConnection(connectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Bornegruppe_ID", borngruppe.Bornegruppe_ID);
                command.Parameters.AddWithValue("@AntalLodSeddelerPrGruppe", amount);
                command.ExecuteNonQuery();
            }
            return borngruppe;
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
        private List<Bornegruppe> QueryMany(string sql)
        {
            var list = new List<Bornegruppe>();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            using var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                var b = new Bornegruppe
                {
                    Bornegruppe_ID = Convert.ToInt32(dataReader["Bornegruppe_ID"]),
                    Gruppenavn = Convert.ToString(dataReader["Gruppenavn"])!,
                    Lokale = Convert.ToString(dataReader["Lokale"])!,
                    Antalborn = Convert.ToInt32(dataReader["Antalborn"]),
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
