using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using Microsoft.Data.Sqlite;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOLederService
{
    public class AdonetLederService
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public AdonetLederService() { }

        public AdonetLederService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Leder>> GetAllLederAsync()
        {
            var lederList = new List<Leder>();
            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            const string sql = "SELECT * FROM Leder";
            using var command = new SqliteCommand(sql, connection);
            using var dataReader = await command.ExecuteReaderAsync();

            while (await dataReader.ReadAsync())
            {
                var leder = new Leder
                {
                    Leder_ID = Convert.ToInt32(dataReader["Leder_ID"]),
                    Navn = Convert.ToString(dataReader["Navn"]) ?? "",
                    Adresse = Convert.ToString(dataReader["Adresse"]) ?? "",
                    Telefon = Convert.ToString(dataReader["Telefon"]) ?? "",
                    Email = Convert.ToString(dataReader["Email"]) ?? "",
                    ErLotteriBestyrer = ToBool(dataReader["ErLotteriBestyrer"]),
                    Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"])
                };

                lederList.Add(leder);
            }

            return lederList;
        }

        public Leder CreateLeder(Leder leder)
        {
            const string sql = @"
INSERT INTO Leder (Navn, Adresse, Telefon, Email, ErLotteriBestyrer, Børnegruppe_ID)
VALUES (@Navn, @Adresse, @Telefon, @Email, @ErLotteriBestyrer, @Børnegruppe_ID);
SELECT last_insert_rowid();";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var cmd = new SqliteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Navn", leder.Navn ?? "");
            cmd.Parameters.AddWithValue("@Adresse", leder.Adresse ?? "");
            cmd.Parameters.AddWithValue("@Telefon", leder.Telefon ?? "");
            cmd.Parameters.AddWithValue("@Email", leder.Email ?? "");
            cmd.Parameters.AddWithValue("@ErLotteriBestyrer", leder.ErLotteriBestyrer ? 1 : 0);
            cmd.Parameters.AddWithValue("@Børnegruppe_ID", leder.Børnegruppe_ID);

            var newId = (long)cmd.ExecuteScalar()!;
            leder.Leder_ID = (int)newId;
            return leder;
        }


        public Leder GetLederByID(int Leder_ID)
        {
            var leder = new Leder();

            const string sql = "SELECT * FROM Leder WHERE Leder_ID = @Leder_ID";
            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Leder_ID", Leder_ID);

            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                leder.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                leder.Navn = Convert.ToString(reader["Navn"]) ?? "";
                leder.Adresse = Convert.ToString(reader["Adresse"]) ?? "";
                leder.Telefon = Convert.ToString(reader["Telefon"]) ?? "";
                leder.Email = Convert.ToString(reader["Email"]) ?? "";
                leder.ErLotteriBestyrer = ToBool(reader["ErLotteriBestyrer"]);
                leder.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);
            }

            return leder;
        }

        public Leder DeleteLeder(Leder leder)
        {
            const string sql = "DELETE FROM Leder WHERE Leder_ID = @Leder_ID";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);

            connection.Open();
            command.Parameters.AddWithValue("@Leder_ID", leder.Leder_ID);
            command.ExecuteNonQuery();

            return leder;
        }

        public Leder UpdateLeder(Leder leder)
        {
            const string sql =
                "UPDATE Leder SET Leder_ID = @Leder_ID, Navn = @Navn, Adresse = @Adresse, Telefon = @Telefon, " +
                "Email = @Email, ErLotteriBestyrer = @ErLotteriBestyrer, Børnegruppe_ID = @Børnegruppe_ID " +
                "WHERE Leder_ID = @Leder_ID";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);

            connection.Open();

            command.Parameters.AddWithValue("@Leder_ID", leder.Leder_ID);
            command.Parameters.AddWithValue("@Navn", leder.Navn ?? "");
            command.Parameters.AddWithValue("@Adresse", leder.Adresse ?? "");
            command.Parameters.AddWithValue("@Telefon", leder.Telefon ?? "");
            command.Parameters.AddWithValue("@Email", leder.Email ?? "");
            command.Parameters.AddWithValue("@ErLotteriBestyrer", leder.ErLotteriBestyrer ? 1 : 0);
            command.Parameters.AddWithValue("@Børnegruppe_ID", leder.Børnegruppe_ID);

            command.ExecuteNonQuery();

            return leder;
        }

        public List<Leder> GetLederByName(string Navn)
        {
            var lederListS = new List<Leder>();
            const string sql = "SELECT * FROM Leder WHERE Navn LIKE @Navn";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Navn", Navn);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var leder = new Leder
                {
                    Leder_ID = Convert.ToInt32(reader["Leder_ID"]),
                    Navn = Convert.ToString(reader["Navn"]) ?? "",
                    Adresse = Convert.ToString(reader["Adresse"]) ?? "",
                    Telefon = Convert.ToString(reader["Telefon"]) ?? "",
                    Email = Convert.ToString(reader["Email"]) ?? "",
                    ErLotteriBestyrer = ToBool(reader["ErLotteriBestyrer"]),
                    Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"])
                };
                lederListS.Add(leder);
            }

            return lederListS;
        }

        public bool LederIdEksisterer(string lederID)
        {
            const string sql = "SELECT COUNT(*) FROM Leder WHERE Leder_ID = @Leder_ID";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Leder_ID", lederID);

            var countObj = command.ExecuteScalar();
            var count = Convert.ToInt32(countObj);
            return count > 0;
        }

        public List<Leder> GelAllLederNavnDescending()
        {
            return QueryMany("SELECT * FROM Leder ORDER BY Navn DESC");
        }

        public List<Leder> GetAllLederNavnAscending()
        {
            return QueryMany("SELECT * FROM Leder ORDER BY Navn ASC");
        }

        public List<Børnegruppe> GetBørneIDOptions()
        {
            var børneIDOptions = new List<Børnegruppe>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            const string sql = "SELECT Børnegruppe_ID, Gruppenavn FROM Børnegruppe ORDER BY Gruppenavn";
            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var børnegruppe = new Børnegruppe
                {
                    Børnegruppe_ID = reader.GetInt32(0),
                    Gruppenavn = reader.GetString(1)
                };

                børneIDOptions.Add(børnegruppe);
            }

            return børneIDOptions;
        }

        // Helpers
        private static bool ToBool(object value)
        {
            // SQLite kan levere 0/1 (long/int) eller bool – denne helper dækker begge
            if (value is bool b) return b;
            return Convert.ToInt32(value) != 0;
        }

        private List<Leder> QueryMany(string sql)
        {
            var list = new List<Leder>();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            using var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                var leder = new Leder
                {
                    Leder_ID = Convert.ToInt32(dataReader["Leder_ID"]),
                    Navn = Convert.ToString(dataReader["Navn"]) ?? "",
                    Adresse = Convert.ToString(dataReader["Adresse"]) ?? "",
                    Telefon = Convert.ToString(dataReader["Telefon"]) ?? "",
                    Email = Convert.ToString(dataReader["Email"]) ?? "",
                    ErLotteriBestyrer = ToBool(dataReader["ErLotteriBestyrer"]),
                    Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"])
                };
                list.Add(leder);
            }

            return list;
        }
    }
}
