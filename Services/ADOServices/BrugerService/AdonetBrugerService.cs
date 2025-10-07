using LodSalgsSystemFDF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;

namespace LodSalgsSystemFDF.Services.ADOServices.BrugerService
{
    public class AdonetBrugerService
    {
        private readonly string connectionString;
        private readonly PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        // Prefer DI – remove the parameterless ctor if you can
        public AdonetBrugerService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Missing DefaultConnection");
        }

        public List<Bruger> GetAllBrugere()
        {
            var brugere = new List<Bruger>();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            const string sql = "SELECT BrugerNavn, Password FROM Bruger ORDER BY BrugerNavn";
            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                brugere.Add(new Bruger
                {
                    BrugerNavn = reader.GetString(0),
                    Password = reader.GetString(1) // this is the HASH in DB
                });
            }
            return brugere;
        }

        public Bruger AddBruger(Bruger bruger)
        {
            if (string.IsNullOrWhiteSpace(bruger.BrugerNavn))
                throw new ArgumentException("BrugerNavn is required");
            if (string.IsNullOrWhiteSpace(bruger.Password))
                throw new ArgumentException("Password is required");

            var navn = bruger.BrugerNavn.Trim();
            var rawPassword = bruger.Password.Trim();

            // Hash and store the hash
            var hash = passwordHasher.HashPassword(navn, rawPassword);

            const string sql = "INSERT INTO Bruger (BrugerNavn, Password) VALUES (@BrugerNavn, @Password)";

            using var connection = new SqliteConnection(connectionString);
            using var command = new SqliteCommand(sql, connection);
            connection.Open();

            command.Parameters.AddWithValue("@BrugerNavn", navn);
            command.Parameters.AddWithValue("@Password", hash);

            command.ExecuteNonQuery();

            // Return the user object with the hashed password if you want
            return new Bruger { BrugerNavn = navn, Password = hash };
        }

        // Optional: helper for verifying during login
        public bool VerifyPassword(string brugerNavn, string rawPassword, string storedHash)
        {
            var result = passwordHasher.VerifyHashedPassword(brugerNavn.Trim(), storedHash, rawPassword.Trim());
            return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}

