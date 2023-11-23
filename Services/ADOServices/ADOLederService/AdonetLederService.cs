using LodSalgsSystemFDF.Models;
using System.Data.SqlClient;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOLederService
{
    public class AdonetLederService
    {
        private IConfiguration configuration { get; }
        string connectionString;

        public AdonetLederService() { }

        public AdonetLederService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("Datacraft.dk");
        }
        public List<Leder> GetAllLeder()
        {
            List<Leder> lederList = new List<Leder>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Leder";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Leder leder = new Leder();
                        leder.Leder_ID = Convert.ToInt32(dataReader["Leder_ID"]);
                        leder.Navn = Convert.ToString(dataReader["Navn"]);
                        leder.Adresse = Convert.ToString(dataReader["Adresse"]);
                        leder.Telefon = Convert.ToString(dataReader["Telefon"]);
                        leder.Email = Convert.ToString(dataReader["Email"]);
                        leder.ErLotteriBestyrer = Convert.ToBoolean(dataReader["ErLotteriBestyrer"]);
                        leder.Ark_ID = Convert.ToInt32(dataReader["Ark_ID"]);
                        leder.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);
                        
                        lederList.Add(leder);


                    }
                }
            }
            return lederList;
        }
    }
}
