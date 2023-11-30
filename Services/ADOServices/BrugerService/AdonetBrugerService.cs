using LodSalgsSystemFDF.Models;
using System.Data.SqlClient;

namespace LodSalgsSystemFDF.Services.ADOServices.BrugerService
{
    public class AdonetBrugerService
    {
        private IConfiguration configuration { get; }
        string connectionString;

        public AdonetBrugerService() { }

        public AdonetBrugerService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("Datacraft.dk");
        }

        public List<Bruger> GetAllBrugere()
        {
            List<Bruger> brugerList = new List<Bruger>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Bruger";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bruger bruger = new Bruger();
                        bruger.BrugerNavn = reader.GetString(0);
                        bruger.Password = reader.GetString(1);
                        

                        brugerList.Add(bruger);


                    }
                }
            }
            return brugerList;
        }

        public Bruger AddBruger(Bruger bruger)
        {
            List<Bruger> brugerList = new List<Bruger>();
            string sql = "INSERT INTO dbo.Bruger (BrugerNavn, Password) VALUES(@BrugerNavn, @Password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@BrugerNavn", bruger.BrugerNavn);
                    command.Parameters.AddWithValue("@Password", bruger.Password);
                    brugerList.Add(bruger);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return bruger;
        }
    }
}
