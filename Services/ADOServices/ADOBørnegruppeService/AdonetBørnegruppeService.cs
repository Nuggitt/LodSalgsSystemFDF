using LodSalgsSystemFDF.Models;
using System.Data.SqlClient;
namespace LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService
{
    public class AdonetBørnegruppeService
    {
        private IConfiguration configuration { get; }
        string connectionString ;

        public AdonetBørnegruppeService() { }

        public AdonetBørnegruppeService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("Datacraft.dk");
        }
        public List<Børnegruppe> GetAllBørnegruppe()
        {
            //IEnumerable<Børnegruppe> lstbørnegruppe = new IEnumerable<Børnegruppe>();
            List<Børnegruppe> lstbørnegruppe = new List<Børnegruppe>();
            string sql = "Select * from Børnegruppe";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Børnegruppe børnegruppe = new Børnegruppe();
                        børnegruppe.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);
                        børnegruppe.Gruppenavn = Convert.ToString(dataReader["Gruppenavn"]);
                        børnegruppe.Lokale = Convert.ToString(dataReader["Lokale"]);
                        børnegruppe.Antalbørn = Convert.ToInt32(dataReader["AntalBørn"]);
                        børnegruppe.Leder_ID = Convert.ToInt32(dataReader["Leder_ID"]);
                        børnegruppe.AntalLodSeddelerPrGruppe = Convert.ToInt32(dataReader["AntalLodSeddelerPrGruppe"]);
                        børnegruppe.AntalSolgteLodSeddeler = Convert.ToInt32(dataReader["AntalSolgteLodSeddeler"]);

                        lstbørnegruppe.Add(børnegruppe);
                    }
                }
            }
            return lstbørnegruppe;
        }

    }
}
