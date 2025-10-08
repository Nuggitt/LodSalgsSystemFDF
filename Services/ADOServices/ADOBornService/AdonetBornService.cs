using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using Microsoft.Data.Sqlite;
using System.Transactions;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBornService
{
    public class AdonetBornService
    {
        private IConfiguration configuration { get; }
        private readonly string connectionString;

        public AdonetBornService() { }

        public AdonetBornService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Born>> GetAllBorn()
        {
            var listborn = new List<Born>();
            const string sql = "SELECT * FROM Born";
            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var born = new Born
                {
                    Born_ID = reader.GetInt32(reader.GetOrdinal("Born_ID")),
                    Navn = reader.GetString(reader.GetOrdinal("Navn")),
                    Adresse = reader.GetString(reader.GetOrdinal("Adresse")),
                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                    GivetLodsedler = reader.GetInt32(reader.GetOrdinal("GivetLodsedler")),
                    AntalSolgteLodseddeler = reader.GetInt32(reader.GetOrdinal("AntalSolgteLodseddeler")),
                    Bornegruppe_ID = reader.GetInt32(reader.GetOrdinal("Bornegruppe_ID"))
                };
                listborn.Add(born);
            }
            return listborn;
        }

        public async Task<Born> GetBorn(int id)
        {
            var born = new Born();
            const string sql = "SELECT * FROM Born WHERE Born_ID = @Born_ID";

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Born_ID", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                born.Born_ID = reader.GetInt32(reader.GetOrdinal("Born_ID"));
                born.Navn = reader.GetString(reader.GetOrdinal("Navn"));
                born.Adresse = reader.GetString(reader.GetOrdinal("Adresse"));
                born.Telefon = reader.GetString(reader.GetOrdinal("Telefon"));
                born.GivetLodsedler = reader.GetInt32(reader.GetOrdinal("GivetLodsedler"));
                born.AntalSolgteLodseddeler = reader.GetInt32(reader.GetOrdinal("AntalSolgteLodseddeler"));
                born.Bornegruppe_ID = reader.GetInt32(reader.GetOrdinal("Bornegruppe_ID"));
            }
            return born;
        }

        public Born CreateBorn(Born born)
        {
            if (born.AntalSolgteLodseddeler < 0 || born.Bornegruppe_ID <= 0)
                throw new NegativeAmountExceptioncs("Værdi må ikke være negativt");

            if (TjekIdEksisterer(born.Born_ID.ToString()))
                throw new DuplicateKeyException(" ID Eksisterer allerede, brug en anden.");

            const string sql = @"
INSERT INTO Born (Navn, Adresse, Telefon, Bornegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler)
VALUES (@Navn, @Adresse, @Telefon, @Bornegruppe_ID, @GivetLodsedler, @AntalSolgteLodseddeler);
SELECT last_insert_rowid();";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Navn", born.Navn);
            command.Parameters.AddWithValue("@Adresse", born.Adresse);
            command.Parameters.AddWithValue("@Telefon", born.Telefon);
            command.Parameters.AddWithValue("@Bornegruppe_ID", born.Bornegruppe_ID);
            command.Parameters.AddWithValue("@GivetLodsedler", born.GivetLodsedler);
            command.Parameters.AddWithValue("@AntalSolgteLodseddeler", born.AntalSolgteLodseddeler);

            var newId = (long)command.ExecuteScalar()!;
            born.Born_ID = Convert.ToInt32(newId);

            return born;
        }

        public bool TjekIdEksisterer(string bornId)
        {
            const string sql = "SELECT COUNT(*) FROM Born WHERE Born_ID = @Born_ID";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Born_ID", bornId);
            var count = (long)command.ExecuteScalar()!;
            return count > 0;
        }

        public Born DeleteBorn(Born born)
        {
            const string sql = "DELETE FROM Born WHERE Born_ID = @Born_ID";
            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            connection.Open();

            command.Parameters.AddWithValue("@Born_ID", born.Born_ID);
            command.ExecuteNonQuery();
            return born;
        }

        public Born UpdateBorn(Born born)
        {
            const string sql = @"
UPDATE Born
SET Navn=@Navn, Adresse=@Adresse, Telefon=@Telefon, GivetLodsedler=@GivetLodsedler,
    AntalSolgteLodseddeler=@AntalSolgteLodseddeler, Bornegruppe_ID=@Bornegruppe_ID
WHERE Born_ID=@Born_ID";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            connection.Open();

            command.Parameters.AddWithValue("@Born_ID", born.Born_ID);
            command.Parameters.AddWithValue("@Navn", born.Navn);
            command.Parameters.AddWithValue("@Adresse", born.Adresse);
            command.Parameters.AddWithValue("@Telefon", born.Telefon);
            command.Parameters.AddWithValue("@GivetLodsedler", born.GivetLodsedler);
            command.Parameters.AddWithValue("@AntalSolgteLodseddeler", born.AntalSolgteLodseddeler);
            command.Parameters.AddWithValue("@Bornegruppe_ID", born.Bornegruppe_ID);

            command.ExecuteNonQuery();
            return born;
        }

        // ---- Sorteringer ----

        private static Born MapBorn(SqliteDataReader r) => new Born
        {
            Born_ID = r.GetInt32(r.GetOrdinal("Born_ID")),
            Navn = r.GetString(r.GetOrdinal("Navn")),
            Adresse = r.GetString(r.GetOrdinal("Adresse")),
            Telefon = r.GetString(r.GetOrdinal("Telefon")),
            GivetLodsedler = r.GetInt32(r.GetOrdinal("GivetLodsedler")),
            AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler")),
            Bornegruppe_ID = r.GetInt32(r.GetOrdinal("Bornegruppe_ID"))
        };

        private List<Born> GetAllOrderBy(string orderBy)
        {
            var list = new List<Born>();
            var sql = $"SELECT * FROM Born ORDER BY {orderBy}";
            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(MapBorn(r));
            return list;
        }

        public List<Born> GetAllBornNavnDescending() => GetAllOrderBy("Navn DESC");
        public List<Born> GetAllBornIDDescending() => GetAllOrderBy("Born_ID DESC");
        public List<Born> GetAllBornAntalSolgteLodseddelerDescending() => GetAllOrderBy("AntalSolgteLodseddeler DESC");
        public List<Born> GetAllBornGruppeIDDescending() => GetAllOrderBy("Bornegruppe_ID DESC");
        public List<Born> GetAllBornNavnAscending() => GetAllOrderBy("Navn ASC");
        public List<Born> GetAllBornIDAscending() => GetAllOrderBy("Born_ID ASC");
        public List<Born> GetAllBornAntalSolgteLodseddelerAscending() => GetAllOrderBy("AntalSolgteLodseddeler ASC");
        public List<Born> GetAllBornGruppeIDAscending() => GetAllOrderBy("Bornegruppe_ID ASC");
        public List<Born> GetGivetLodsedlerDescending() => GetAllOrderBy("GivetLodsedler DESC");
        public List<Born> GetGivetLodsedlerAscending() => GetAllOrderBy("GivetLodsedler ASC");

        public List<Born> GetBornByName(string name)
        {
            var list = new List<Born>();
            const string sql = "SELECT * FROM Born WHERE Navn LIKE @Navn";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Navn", name);
            using var reader = command.ExecuteReader();
            while (reader.Read()) list.Add(MapBorn(reader));
            return list;
        }

        public Born TildelLodsedler(Born born, int amount)
        {
            const string sqlBorn = "UPDATE Born SET GivetLodsedler = GivetLodsedler + @GivetLodsedler WHERE Born_ID = @Born_ID";
            const string sqlGruppe = "UPDATE Bornegruppe SET AntalLodSeddelerPrGruppe = AntalLodSeddelerPrGruppe - @AntalLodSeddelerPrGruppe WHERE Bornegruppe_ID = @Bornegruppe_ID";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var tx = connection.BeginTransaction();
            try
            {
                using (var cmd = new SqliteCommand(sqlBorn, connection, tx))
                {
                    cmd.Parameters.AddWithValue("@Born_ID", born.Born_ID);
                    cmd.Parameters.AddWithValue("@GivetLodsedler", amount);
                    cmd.ExecuteNonQuery();
                }

                if (amount > 0)
                {
                    using var cmd2 = new SqliteCommand(sqlGruppe, connection, tx);
                    cmd2.Parameters.AddWithValue("@Bornegruppe_ID", born.Bornegruppe_ID);
                    cmd2.Parameters.AddWithValue("@AntalLodSeddelerPrGruppe", amount);
                    cmd2.ExecuteNonQuery();
                }

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
            return born;
        }

        public async Task<IEnumerable<Born>> GetBornInBornegruppe(int id)
        {
            var list = new List<Born>();
            const string sql = "SELECT * FROM Born WHERE Bornegruppe_ID = @Bornegruppe_ID";

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Bornegruppe_ID", id);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) list.Add(MapBorn(reader));
            return list;
        }

        public async Task<IEnumerable<Born>> GetBornInBornegruppeByID(int id)
        {
            var list = new List<Born>();
            const string sql = "SELECT * FROM Born WHERE Born_ID = @Born_ID";

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Born_ID", id);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) list.Add(MapBorn(reader));
            return list;
        }

        public List<Bornegruppe> GetBornegruppeOptions()
        {
            var options = new List<Bornegruppe>();
            const string sql = "SELECT Bornegruppe_ID, Gruppenavn FROM Bornegruppe ORDER BY Gruppenavn";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                options.Add(new Bornegruppe
                {
                    Bornegruppe_ID = reader.GetInt32(0),
                    Gruppenavn = reader.GetString(1)
                });
            }
            return options;
        }
    }
}
