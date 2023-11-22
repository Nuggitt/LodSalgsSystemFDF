using LodSalgsSystemFDF.Models;
using System.Data.SqlClient;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOSalgService
{
    public class AdonetSalgService
    {
        private IConfiguration configuration { get; }
        string connectionString;

        public AdonetSalgService() { }

        public AdonetSalgService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("Datacraft.dk");
        }

        public List<Salg> GetAllSalgs()
        {
            List<Salg> salgList = new List<Salg>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Salg";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Salg salg = new Salg();
                        salg.Salg_ID = reader.GetInt32(0);
                        salg.Ark_ID = reader.GetInt32(1);
                        salg.Børn_ID = reader.GetInt32(2);
                        salg.Pris = reader.GetDouble(3);

                        salgList.Add(salg);


                    }
                }
            }
            return salgList;
        }

        public Salg CreateSalg(Salg salg)
        {
            List<Salg> salgs = new List<Salg>();
            string sql = "INSERT INTO dbo.Salg (Salg_ID, Ark_ID, Børn_ID, Pris) VALUES(@Salg_ID, @Ark_ID, @Børn_ID, @Pris)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql,connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Salg_ID", salg.Salg_ID);
                    command.Parameters.AddWithValue("@Ark_ID", salg.Ark_ID);
                    command.Parameters.AddWithValue("@Børn_ID", salg.Børn_ID);
                    command.Parameters.AddWithValue("@Pris", salg.Pris);
                    salgs.Add(salg);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return salg;
        }
    }
}
