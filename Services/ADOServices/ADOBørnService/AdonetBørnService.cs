using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using Microsoft.Data.Sqlite;
using System.Transactions;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBørnService
{
    public class AdonetBørnService
    {
        private IConfiguration configuration { get; }
        private readonly string connectionString;

        public AdonetBørnService() { }

        public AdonetBørnService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Børn>> GetAllBørn()
        {
            var listbørn = new List<Børn>();
            const string sql = "SELECT * FROM Børn";
            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var børn = new Børn
                {
                    Børn_ID = reader.GetInt32(reader.GetOrdinal("Børn_ID")),
                    Navn = reader.GetString(reader.GetOrdinal("Navn")),
                    Adresse = reader.GetString(reader.GetOrdinal("Adresse")),
                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                    GivetLodsedler = reader.GetInt32(reader.GetOrdinal("GivetLodsedler")),
                    AntalSolgteLodseddeler = reader.GetInt32(reader.GetOrdinal("AntalSolgteLodseddeler")),
                    Børnegruppe_ID = reader.GetInt32(reader.GetOrdinal("Børnegruppe_ID"))
                };
                listbørn.Add(børn);
            }
            return listbørn;
        }

        public async Task<Børn> GetBørn(int id)
        {
            var børn = new Børn();
            const string sql = "SELECT * FROM Børn WHERE Børn_ID = @Børn_ID";

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Børn_ID", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                børn.Børn_ID = reader.GetInt32(reader.GetOrdinal("Børn_ID"));
                børn.Navn = reader.GetString(reader.GetOrdinal("Navn"));
                børn.Adresse = reader.GetString(reader.GetOrdinal("Adresse"));
                børn.Telefon = reader.GetString(reader.GetOrdinal("Telefon"));
                børn.GivetLodsedler = reader.GetInt32(reader.GetOrdinal("GivetLodsedler"));
                børn.AntalSolgteLodseddeler = reader.GetInt32(reader.GetOrdinal("AntalSolgteLodseddeler"));
                børn.Børnegruppe_ID = reader.GetInt32(reader.GetOrdinal("Børnegruppe_ID"));
            }
            return børn;
        }

        public Børn CreateBørn(Børn børn)
        {
            if (børn.AntalSolgteLodseddeler < 0 || børn.Børnegruppe_ID <= 0)
                throw new NegativeAmountExceptioncs("Værdi må ikke være negativt");

            if (TjekIdEksisterer(børn.Børn_ID.ToString()))
                throw new DuplicateKeyException(" ID Eksisterer allerede, brug en anden.");

            const string sql = @"
INSERT INTO Børn (Navn, Adresse, Telefon, Børnegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler)
VALUES (@Navn, @Adresse, @Telefon, @Børnegruppe_ID, @GivetLodsedler, @AntalSolgteLodseddeler);
SELECT last_insert_rowid();";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Navn", børn.Navn);
            command.Parameters.AddWithValue("@Adresse", børn.Adresse);
            command.Parameters.AddWithValue("@Telefon", børn.Telefon);
            command.Parameters.AddWithValue("@Børnegruppe_ID", børn.Børnegruppe_ID);
            command.Parameters.AddWithValue("@GivetLodsedler", børn.GivetLodsedler);
            command.Parameters.AddWithValue("@AntalSolgteLodseddeler", børn.AntalSolgteLodseddeler);

            var newId = (long)command.ExecuteScalar()!;
            børn.Børn_ID = Convert.ToInt32(newId);

            return børn;
        }

        public bool TjekIdEksisterer(string børnId)
        {
            const string sql = "SELECT COUNT(*) FROM Børn WHERE Børn_ID = @Børn_ID";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Børn_ID", børnId);
            var count = (long)command.ExecuteScalar()!;
            return count > 0;
        }

        public Børn DeleteBørn(Børn børn)
        {
            const string sql = "DELETE FROM Børn WHERE Børn_ID = @Børn_ID";
            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            connection.Open();

            command.Parameters.AddWithValue("@Børn_ID", børn.Børn_ID);
            command.ExecuteNonQuery();
            return børn;
        }

        public Børn UpdateBørn(Børn børn)
        {
            const string sql = @"
UPDATE Børn
SET Navn=@Navn, Adresse=@Adresse, Telefon=@Telefon, GivetLodsedler=@GivetLodsedler,
    AntalSolgteLodseddeler=@AntalSolgteLodseddeler, Børnegruppe_ID=@Børnegruppe_ID
WHERE Børn_ID=@Børn_ID";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            connection.Open();

            command.Parameters.AddWithValue("@Børn_ID", børn.Børn_ID);
            command.Parameters.AddWithValue("@Navn", børn.Navn);
            command.Parameters.AddWithValue("@Adresse", børn.Adresse);
            command.Parameters.AddWithValue("@Telefon", børn.Telefon);
            command.Parameters.AddWithValue("@GivetLodsedler", børn.GivetLodsedler);
            command.Parameters.AddWithValue("@AntalSolgteLodseddeler", børn.AntalSolgteLodseddeler);
            command.Parameters.AddWithValue("@Børnegruppe_ID", børn.Børnegruppe_ID);

            command.ExecuteNonQuery();
            return børn;
        }

        // ---- Sorteringer ----

        private static Børn MapBørn(SqliteDataReader r) => new Børn
        {
            Børn_ID = r.GetInt32(r.GetOrdinal("Børn_ID")),
            Navn = r.GetString(r.GetOrdinal("Navn")),
            Adresse = r.GetString(r.GetOrdinal("Adresse")),
            Telefon = r.GetString(r.GetOrdinal("Telefon")),
            GivetLodsedler = r.GetInt32(r.GetOrdinal("GivetLodsedler")),
            AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler")),
            Børnegruppe_ID = r.GetInt32(r.GetOrdinal("Børnegruppe_ID"))
        };

        private List<Børn> GetAllOrderBy(string orderBy)
        {
            var list = new List<Børn>();
            var sql = $"SELECT * FROM Børn ORDER BY {orderBy}";
            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(MapBørn(r));
            return list;
        }

        public List<Børn> GetAllBørnNavnDescending() => GetAllOrderBy("Navn DESC");
        public List<Børn> GetAllBørnIDDescending() => GetAllOrderBy("Børn_ID DESC");
        public List<Børn> GetAllBørnAntalSolgteLodseddelerDescending() => GetAllOrderBy("AntalSolgteLodseddeler DESC");
        public List<Børn> GetAllBørnGruppeIDDescending() => GetAllOrderBy("Børnegruppe_ID DESC");
        public List<Børn> GetAllBørnNavnAscending() => GetAllOrderBy("Navn ASC");
        public List<Børn> GetAllBørnIDAscending() => GetAllOrderBy("Børn_ID ASC");
        public List<Børn> GetAllBørnAntalSolgteLodseddelerAscending() => GetAllOrderBy("AntalSolgteLodseddeler ASC");
        public List<Børn> GetAllBørnGruppeIDAscending() => GetAllOrderBy("Børnegruppe_ID ASC");
        public List<Børn> GetGivetLodsedlerDescending() => GetAllOrderBy("GivetLodsedler DESC");
        public List<Børn> GetGivetLodsedlerAscending() => GetAllOrderBy("GivetLodsedler ASC");

        public List<Børn> GetBørnByName(string name)
        {
            var list = new List<Børn>();
            const string sql = "SELECT * FROM Børn WHERE Navn LIKE @Navn";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Navn", name);
            using var reader = command.ExecuteReader();
            while (reader.Read()) list.Add(MapBørn(reader));
            return list;
        }

        public Børn TildelLodsedler(Børn børn, int amount)
        {
            const string sqlBørn = "UPDATE Børn SET GivetLodsedler = GivetLodsedler + @GivetLodsedler WHERE Børn_ID = @Børn_ID";
            const string sqlGruppe = "UPDATE Børnegruppe SET AntalLodSeddelerPrGruppe = AntalLodSeddelerPrGruppe - @AntalLodSeddelerPrGruppe WHERE Børnegruppe_ID = @Børnegruppe_ID";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var tx = connection.BeginTransaction();
            try
            {
                using (var cmd = new SqliteCommand(sqlBørn, connection, tx))
                {
                    cmd.Parameters.AddWithValue("@Børn_ID", børn.Børn_ID);
                    cmd.Parameters.AddWithValue("@GivetLodsedler", amount);
                    cmd.ExecuteNonQuery();
                }

                if (amount > 0)
                {
                    using var cmd2 = new SqliteCommand(sqlGruppe, connection, tx);
                    cmd2.Parameters.AddWithValue("@Børnegruppe_ID", børn.Børnegruppe_ID);
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
            return børn;
        }

        public async Task<IEnumerable<Børn>> GetBørnInBørnegruppe(int id)
        {
            var list = new List<Børn>();
            const string sql = "SELECT * FROM Børn WHERE Børnegruppe_ID = @Børnegruppe_ID";

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Børnegruppe_ID", id);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) list.Add(MapBørn(reader));
            return list;
        }

        public async Task<IEnumerable<Børn>> GetBørnInBørnegruppeByID(int id)
        {
            var list = new List<Børn>();
            const string sql = "SELECT * FROM Børn WHERE Børn_ID = @Børn_ID";

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Børn_ID", id);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) list.Add(MapBørn(reader));
            return list;
        }

        public List<Børnegruppe> GetBørnegruppeOptions()
        {
            var options = new List<Børnegruppe>();
            const string sql = "SELECT Børnegruppe_ID, Gruppenavn FROM Børnegruppe ORDER BY Gruppenavn";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                options.Add(new Børnegruppe
                {
                    Børnegruppe_ID = reader.GetInt32(0),
                    Gruppenavn = reader.GetString(1)
                });
            }
            return options;
        }
    }
}
