using LodSalgsSystemFDF.Models;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBørnService

{
    public class AdonetBørnService
    {
        private IConfiguration configuration { get; }
        string connectionString;

        public AdonetBørnService() { }

        public AdonetBørnService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("Datacraft.dk");
        }

        public List<Børn> GetAllBørn()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "Select * from Børn";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Børn børn = new Børn();
                        børn.Børn_ID = Convert.ToInt32(dataReader["Børn_ID"]);
                        børn.Navn = Convert.ToString(dataReader["Navn"]);
                        børn.Adresse = Convert.ToString(dataReader["Adresse"]);
                        børn.Telefon = Convert.ToString(dataReader["Telefon"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);


                    }

                }

            }

            return listbørn;
        }

        public Børn GetBørn(int id)
        {
            List<Børn> listbørn = new List<Børn>();
            Børn børn = new Børn();
            string sql = "Select * FROM dbo.Børn WHERE Børn_ID = @Børn_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Børn_ID", id);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        børn.Børn_ID = Convert.ToInt32(dataReader["Børn_ID"]);
                        børn.Navn = Convert.ToString(dataReader["Navn"]);
                        børn.Adresse = Convert.ToString(dataReader["Adresse"]);
                        børn.Telefon = Convert.ToString(dataReader["Telefon"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }



            }
            return børn;
        }

        public Børn CreateBørn(Børn børn)
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "INSERT INTO Børn (Børn_ID, Navn, Adresse, Telefon, AntalSolgteLodseddeler, Børnegruppe_ID) VALUES(@Børn_ID, @Navn, @Adresse, @Telefon, @AntalSolgteLodseddeler, @Børnegruppe_ID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Børn_ID", børn.Børn_ID);
                    command.Parameters.AddWithValue("@Navn", børn.Navn);
                    command.Parameters.AddWithValue("@Adresse", børn.Adresse);
                    command.Parameters.AddWithValue("@Telefon", børn.Telefon);
                    command.Parameters.AddWithValue("@AntalSolgteLodseddeler", børn.AntalSolgteLodseddeler);
                    command.Parameters.AddWithValue("@Børnegruppe_ID", børn.Børnegruppe_ID);

                    listbørn.Add(børn);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return børn;
        }

        public Børn DeleteBørn(Børn børn)
        {
            List<Børn> Børn = new List<Børn>();
            string sql = "DELETE FROM Børn WHERE Børn_ID = @Børn_ID";

            using SqlConnection connection = new SqlConnection(connectionString);

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Børn_ID", børn.Børn_ID);

                int numberOfRowsAffected = command.ExecuteNonQuery();
            }
            return børn;
        }

        public Børn UpdateBørn(Børn børn)
        {
            string sql = "UPDATE SET Børn Navn = @Navn, Adresse = @Adresse, Telefon = @Telefon, AntalSolgteLodseddeler = @AntalSolgteLodseddeler, Børnegruppe_ID = @Børnegruppe_ID WHERE Børn_ID = Børn_ID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Børn_ID", børn.Børn_ID);
                    command.Parameters.AddWithValue("@Navn", børn.Navn);
                    command.Parameters.AddWithValue("@Adresse", børn.Adresse);
                    command.Parameters.AddWithValue("@Telefon", børn.Telefon);
                    command.Parameters.AddWithValue("@AntalSolgteLodseddeler", børn.AntalSolgteLodseddeler);
                    command.Parameters.AddWithValue("@Børnegruppe_ID", børn.Børnegruppe_ID);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return børn;
        }

    }

   

}

