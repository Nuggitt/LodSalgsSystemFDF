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
                        børn.Ark_ID = Convert.ToInt32(dataReader["Ark_ID"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);


                    }

                }

            }

            return listbørn;
        }

    }
}

