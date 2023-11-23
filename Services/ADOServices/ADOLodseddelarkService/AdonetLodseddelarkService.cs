using LodSalgsSystemFDF.Models;
using System.Data.SqlClient;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOLodseddelarkService
{
    public class AdonetLodseddelarkService
    {
        private IConfiguration configuration { get; }
        string connectionString;

        public AdonetLodseddelarkService() { }

        public AdonetLodseddelarkService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("Datacraft.dk");
        }
        public List<Lodseddelark> GetAllLodseddelark()
        {
            //IEnumerable<Børnegruppe> lstbørnegruppe = new IEnumerable<Børnegruppe>();
            List<Lodseddelark> listlodseddelark = new List<Lodseddelark>();
            string sql = "Select * from Lodseddelark";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Lodseddelark lodseddelark = new Lodseddelark();
                        lodseddelark.Ark_ID = Convert.ToInt32(dataReader["Ark_ID"]);
                        lodseddelark.AntalLodSeddeler = Convert.ToInt32(dataReader["AntalLodSeddeler"]);
                        lodseddelark.PrisPrLod = Convert.ToInt32(dataReader["PrisPrLod"]);
                        lodseddelark.PrisPrArk = Convert.ToInt32(dataReader["PrisPrArk"]);
                        
                        listlodseddelark.Add(lodseddelark);
                    }
                }
            }
            return listlodseddelark;
        }
    }
}
