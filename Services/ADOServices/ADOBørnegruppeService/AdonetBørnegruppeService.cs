using LodSalgsSystemFDF.Models;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            connectionString = configuration.GetConnectionString("datacraft_dk");
        }
        //public List<BørnegruppeService> GetAllBørnegruppe()
        //{
        //    List<Børnegruppe> lstbørnegruppe = new List<Børnegruppe>();
        //    string sql = "Select * from Børnegruppe";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        using (SqlDataReader dataReader = command.ExecuteReader())
        //        {
        //            while (dataReader.Read())
        //            {
        //                Børnegruppe børnegruppe = new Børnegruppe();
        //                børnegruppe.Børnegruppe_Id = Convert.ToInt32(dataReader["Børnegruppe_Id"]);
        //                børnegruppe.Gruppenavn = Convert.ToString(dataReader["Gruppenavn"]);
        //                børnegruppe.Lokale = Convert.ToString(dataReader["Lokale"]);
        //                børnegruppe.Antalbørn = Convert.ToInt32(dataReader["AntalBørn"]);
        //                børnegruppe.LederId = Convert.ToInt32(dataReader["LederID"]);
         
        //                lstbørnegruppe.Add(børnegruppe);
        //            }
        //        }
        //    }
        //    return lstbørnegruppe;
        //}
        
    }
}
