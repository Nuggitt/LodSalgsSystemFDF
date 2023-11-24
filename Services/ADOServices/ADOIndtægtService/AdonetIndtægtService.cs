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
            connectionString = configuration.GetConnectionString("Datacraft.dk");
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
                        indtægt.Salg_ID = Convert.ToInt32(reader["Salg_ID"]);

                        indtægtList.Add(indtægt);


                    }
                }
            }
            return indtægtList;
        }

        public Indtægt GetIndtægtById(int id)
        {
            List<Indtægt> indtægtsList = new List<Indtægt>();
            Indtægt indtægt = new Indtægt();
            string sql = "SELECT * FROM dbo.Indtægt WHERE Indtægt_ID = @Indtægt_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Indtægt_ID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        indtægt.Indtægt_ID = Convert.ToInt32(reader["Indtægt_ID"]);                        
                        indtægt.Salg_ID = Convert.ToInt32(reader["Salg_ID"]);
                        indtægtsList.Add(indtægt);
            
                    }
                }
            }
            return indtægt;
        }

        public Indtægt CreateIndtægt(Indtægt indtægt)
        {
            List<Indtægt> indtægtsList = new List<Indtægt>();
            string sql = "INSERT INTO dbo.Indtægt (Indtægt_ID, Salg_ID) VALUES(@Indtægt_ID, @Salg_ID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Indtægt_ID", indtægt.Indtægt_ID);
                    command.Parameters.AddWithValue("@Salg_ID", indtægt.Salg_ID);
                    indtægtsList.Add(indtægt);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return indtægt;
        }

        public Indtægt DeleteIndtægt(Indtægt indtægt)
        {
            List<Indtægt> indtægtsList = new List<Indtægt>();
            string sql = "DELETE FROM dbo.Indtægt WHERE Indtægt_ID = @Indtægt_ID";

            using SqlConnection connection = new SqlConnection(connectionString);

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Indtægt_ID", indtægt.Indtægt_ID);

                int numberOfRowsAffected = command.ExecuteNonQuery();
            }
            return indtægt;
        }

        public Indtægt UpdateIndtægt(Indtægt indtægt)
        {
            string sql = "UPDATE dbo.Indtægt, Salg_ID = @Salg_ID WHERE Indtægt_ID = @Indtægt_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Indtægt_ID", indtægt.Indtægt_ID);                   
                    command.Parameters.AddWithValue("@Salg_ID", indtægt.Salg_ID);
                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return indtægt;
        }
    }
}
