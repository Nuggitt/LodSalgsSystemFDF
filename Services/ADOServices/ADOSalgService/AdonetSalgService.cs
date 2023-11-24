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
                        salg.Børn_ID = reader.GetInt32(1);
                        salg.Leder_ID = reader.GetInt32(2);
                        salg.Dato = reader.GetDateTime(3);
                        salg.AntalLodseddelerRetur = reader.GetInt32(4);
                        salg.AntalSolgteLodSeddelerPrSalg = reader.GetInt32(5);
                        salg.Pris = reader.GetDouble(6);

                        salgList.Add(salg);


                    }
                }
            }
            return salgList;
        }

        public Salg GetSalgById(int id)
        {
            List<Salg> salgsList = new List<Salg>();
            Salg salg = new Salg();
            string sql = "SELECT * FROM dbo.Salg WHERE Salg_ID = @Salg_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Salg_ID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salg.Salg_ID = Convert.ToInt32(reader["Salg_ID"]);
                        salg.Børn_ID = Convert.ToInt32(reader["Børn_ID"]);
                        salg.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                        salg.Dato = Convert.ToDateTime(reader["Dato"]);
                        salg.AntalLodseddelerRetur = Convert.ToInt32(reader["AntalLodseddelerRetur"]);
                        salg.AntalSolgteLodSeddelerPrSalg = Convert.ToInt32(reader["AntalSolgteLodSeddelerPrSalg"]);
                        salg.Pris = Convert.ToDouble(reader["Pris"]);
                        salgsList.Add(salg);
                    }
                }
            }
            return salg;
        }

        public Salg CreateSalg(Salg salg)
        {
            List<Salg> salgsList = new List<Salg>();
            string sql = "INSERT INTO dbo.Salg (Salg_ID, Børn_ID, Leder_ID, Dato, AntalLodseddelerRetur, AntalSolgteLodSeddelerPrSalg,  Pris) VALUES(@Salg_ID, @Børn_ID, @Leder_ID, @Dato, @AntalLodseddelerRetur, @AntalSolgteLodSeddelerPrSalg,  @Pris)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql,connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Salg_ID", salg.Salg_ID);
                    command.Parameters.AddWithValue("@Børn_ID", salg.Børn_ID);
                    command.Parameters.AddWithValue("@Leder_ID", salg.Leder_ID);
                    command.Parameters.AddWithValue("@Dato", salg.Dato);
                    command.Parameters.AddWithValue("@AntalLodseddelerRetur", salg.AntalLodseddelerRetur);
                    command.Parameters.AddWithValue("@AntalSolgteLodSeddelerPrSalg", salg.AntalSolgteLodSeddelerPrSalg);
                    command.Parameters.AddWithValue("@Pris", salg.Pris);
                    salgsList.Add(salg);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return salg;
        }

        public Salg DeleteSalg(Salg salg)
        {
            List<Salg> salgsList = new List<Salg>();
            string sql = "DELETE FROM dbo.Salg WHERE Salg_ID = @Salg_ID";

            using SqlConnection connection = new SqlConnection(connectionString);

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Salg_Id", salg.Salg_ID);

                int numberOfRowsAffected = command.ExecuteNonQuery();
            }
            return salg;
        }

        public Salg UpdateSalg(Salg salg)
        {
            string sql = "UPDATE dbo.Salg Set Børn_ID = @Børn_ID, Leder_ID = @Leder_ID, Dato = @Dato, AntalLodseddelerRetur = @AntalLodseddelerRetur, AntalSolgteLodSeddelerPrSalg = @AntalSolgteLodSeddelerPrSalg,   Pris = @Pris WHERE Salg_ID = @Salg_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Salg_ID", salg.Salg_ID);
                    command.Parameters.AddWithValue("@Børn_ID", salg.Børn_ID);
                    command.Parameters.AddWithValue("@Leder_ID", salg.Leder_ID);
                    command.Parameters.AddWithValue("@Dato", salg.Dato);
                    command.Parameters.AddWithValue("@AntalLodseddelerRetur", salg.AntalLodseddelerRetur);
                    command.Parameters.AddWithValue("@AntalSolgteLodSeddelerPrSalg", salg.AntalSolgteLodSeddelerPrSalg);
                    command.Parameters.AddWithValue("@Pris", salg.Pris);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return salg;
        }


    }
}
