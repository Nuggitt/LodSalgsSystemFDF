using LodSalgsSystemFDF.Models;
using System.Data.SqlClient;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOIndtægtService
{
    public class AdonetIndtægtService
    {
        private IConfiguration configuration { get; }
        string connectionString;

        public AdonetIndtægtService() { }

        public AdonetIndtægtService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("Datacraft2.dk");
        }

        public List<Indtægt> GetAllIndtægter()
        {
            List<Indtægt> indtægtList = new List<Indtægt>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM [dbo].[Indtægt]";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Indtægt indtægt = new Indtægt();
                        indtægt.Indtægt_ID = Convert.ToInt32(reader["Indtægt_ID"]);
                        indtægt.Dato = Convert.ToDateTime(reader["Dato"]);
                        indtægt.Salg_ID = Convert.ToInt32(reader["Salg_ID"]);

                        indtægtList.Add(indtægt);


                    }
                }
            }
            return indtægtList;
        }
    }
}
