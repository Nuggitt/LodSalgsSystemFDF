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

        //public List<Indtægt> GetAllIndtægter()
        //{
        //    List<Indtægt> indtægtList = new List<Indtægt>();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string sql = "SELECT * FROM [dbo].[Indtægt]";
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Indtægt indtægt = new Indtægt();
        //                indtægt.Indtægt_ID = Convert.ToInt32(reader["Indtægt_ID"]);
        //                indtægt.Salg_ID = Convert.ToInt32(reader["Salg_ID"]);



        //                indtægtList.Add(indtægt);


        //            }
        //        }
        //    }
        //    return indtægtList;
        //}



        public List<Indtægt> GetAllIndtægter()
        {
            List<Indtægt> indtægtList = new List<Indtægt>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Indtægt.Indtægt_ID, Salg.Salg_ID, Salg.Dato, Børn.Børn_ID, Børnegruppe.Børnegruppe_ID, Børnegruppe.Gruppenavn, Børn.Navn, Børn.Adresse, Børn.Telefon, Børn.AntalSolgteLodseddeler, Børnegruppe.AntalSolgteLodSeddeler FROM Indtægt\r\nJOIN Salg ON Indtægt.Salg_ID = Salg.Salg_ID\r\nJOIN Børn ON Salg.Børn_ID = Børn.Børn_ID\r\nJOIN Børnegruppe ON Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Indtægt indtægt = new Indtægt();
                        indtægt.Indtægt_ID = Convert.ToInt32(reader["Indtægt_ID"]);
                        indtægt.Salg_ID = Convert.ToInt32(reader["Salg_ID"]);

                        
                        indtægt.Salg = new Salg();
                        indtægt.Børn = new Børn();
                        indtægt.Børnegruppe = new Børnegruppe();

                        
                        indtægt.Salg.Dato = (DateTime)(reader["Dato"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["Dato"]));
                        indtægt.Børn.Børn_ID = Convert.ToInt32(reader["Børn_ID"]);
                        indtægt.Børnegruppe.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);
                        indtægt.Børnegruppe.Gruppenavn = Convert.ToString(reader["Gruppenavn"]);
                        indtægt.Børn.Navn = Convert.ToString(reader["Navn"]);
                        indtægt.Børn.Adresse = Convert.ToString(reader["Adresse"]);
                        indtægt.Børn.Telefon = Convert.ToString(reader["Telefon"]);
                        indtægt.Børn.AntalSolgteLodseddeler = Convert.ToInt32(reader["AntalSolgteLodseddeler"]);
                        indtægt.Børnegruppe.AntalSolgteLodseddeler = Convert.ToInt32(reader["AntalSolgteLodSeddeler"]);

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
            string sql = "INSERT INTO dbo.Indtægt (Indtægt.Indtægt_ID, Indtægt.Salg_ID) VALUES(@Indtægt_ID, @Salg_ID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Indtægt_ID", indtægt.Indtægt_ID);
                    command.Parameters.AddWithValue("@Salg_ID", indtægt.Salg.Salg_ID);
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
            string sql = "UPDATE dbo.Indtægt SET Salg_ID = @Salg_ID WHERE Indtægt_ID = @Indtægt_ID";

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

        public List<Indtægt> GetIndtægtIDDESC()
        {
            List<Indtægt> indtægtlist = new List<Indtægt>();
            string sql = "Select Indtægt.Indtægt_ID, Salg.Salg_ID, Salg.Dato, Børn.Børn_ID, Børnegruppe.Børnegruppe_ID, Børnegruppe.Gruppenavn,  Børn.Navn, Børn.Adresse, Børn.Telefon, Børn.AntalSolgteLodseddeler, Børnegruppe.AntalSolgteLodSeddeler  FROM Indtægt\r\njoin Salg on Indtægt.Salg_ID = Salg.Salg_ID\r\njoin Børn on Salg.Salg_ID = Børn.Børn_ID\r\njoin Børnegruppe on Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID\r\nORDER BY indtægt.Indtægt_ID ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Indtægt indtægt = new Indtægt();
                        indtægt.Indtægt_ID = Convert.ToInt32(dataReader["Indtægt_ID"]);
                        indtægt.Salg_ID = Convert.ToInt32(dataReader["Salg_ID"]);


                        indtægt.Salg = new Salg();
                        indtægt.Børn = new Børn();
                        indtægt.Børnegruppe = new Børnegruppe();


                        indtægt.Salg.Dato = (DateTime)(dataReader["Dato"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["Dato"]));
                        indtægt.Børn.Børn_ID = Convert.ToInt32(dataReader["Børn_ID"]);
                        indtægt.Børnegruppe.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);
                        indtægt.Børnegruppe.Gruppenavn = Convert.ToString(dataReader["Gruppenavn"]);
                        indtægt.Børn.Navn = Convert.ToString(dataReader["Navn"]);
                        indtægt.Børn.Adresse = Convert.ToString(dataReader["Adresse"]);
                        indtægt.Børn.Telefon = Convert.ToString(dataReader["Telefon"]);
                        indtægt.Børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);

                        indtægtlist.Add(indtægt);
                    }
                }
            }

            return indtægtlist;
        }

        public List<Indtægt> GetIndtægtIDASC()
        {
            List<Indtægt> indtægtlist = new List<Indtægt>();
            string sql = "Select Indtægt.Indtægt_ID, Salg.Salg_ID, Salg.Dato, Børn.Børn_ID, Børnegruppe.Børnegruppe_ID, Børnegruppe.Gruppenavn,  Børn.Navn, Børn.Adresse, Børn.Telefon, Børn.AntalSolgteLodseddeler, Børnegruppe.AntalSolgteLodSeddeler  FROM Indtægt\r\njoin Salg on Indtægt.Salg_ID = Salg.Salg_ID\r\njoin Børn on Salg.Salg_ID = Børn.Børn_ID\r\njoin Børnegruppe on Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID\r\nORDER BY indtægt.Indtægt_ID DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Indtægt indtægt = new Indtægt();
                        indtægt.Indtægt_ID = Convert.ToInt32(dataReader["Indtægt_ID"]);
                        indtægt.Salg_ID = Convert.ToInt32(dataReader["Salg_ID"]);


                        indtægt.Salg = new Salg();
                        indtægt.Børn = new Børn();
                        indtægt.Børnegruppe = new Børnegruppe();


                        indtægt.Salg.Dato = (DateTime)(dataReader["Dato"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["Dato"]));
                        indtægt.Børn.Børn_ID = Convert.ToInt32(dataReader["Børn_ID"]);
                        indtægt.Børnegruppe.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);
                        indtægt.Børnegruppe.Gruppenavn = Convert.ToString(dataReader["Gruppenavn"]);
                        indtægt.Børn.Navn = Convert.ToString(dataReader["Navn"]);
                        indtægt.Børn.Adresse = Convert.ToString(dataReader["Adresse"]);
                        indtægt.Børn.Telefon = Convert.ToString(dataReader["Telefon"]);
                        indtægt.Børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);

                        indtægtlist.Add(indtægt);
                    }
                }
            }

            return indtægtlist;
        }



        public List<Indtægt> GetAntalSolgteLodseddelerDESC()
        {
            List<Indtægt> indtægtlist = new List<Indtægt>();
            string sql = "SELECT\r\n    Indtægt.Indtægt_ID,\r\n    Salg.Salg_ID,\r\n    Salg.Dato,\r\n    Børn.Børn_ID,\r\n    Børnegruppe.Børnegruppe_ID,\r\n    Børnegruppe.Gruppenavn,\r\n    Børn.Navn,\r\n    Børn.Adresse,\r\n    Børn.Telefon,\r\n    Børn.AntalSolgteLodseddeler,\r\n    Børnegruppe.AntalSolgteLodSeddeler\r\nFROM\r\n    Indtægt\r\nJOIN\r\n    Salg ON Indtægt.Salg_ID = Salg.Salg_ID\r\nJOIN\r\n    Børn ON Salg.Børn_ID = Børn.Børn_ID\r\nJOIN\r\n    Børnegruppe ON Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID\r\nORDER BY\r\n    Børn.AntalSolgteLodseddeler DESC;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Indtægt indtægt = new Indtægt();
                        indtægt.Indtægt_ID = Convert.ToInt32(dataReader["Indtægt_ID"]);
                        indtægt.Salg_ID = Convert.ToInt32(dataReader["Salg_ID"]);


                        indtægt.Salg = new Salg();
                        indtægt.Børn = new Børn();
                        indtægt.Børnegruppe = new Børnegruppe();


                        indtægt.Salg.Dato = (DateTime)(dataReader["Dato"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["Dato"]));
                        indtægt.Børn.Børn_ID = Convert.ToInt32(dataReader["Børn_ID"]);
                        indtægt.Børnegruppe.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);
                        indtægt.Børnegruppe.Gruppenavn = Convert.ToString(dataReader["Gruppenavn"]);
                        indtægt.Børn.Navn = Convert.ToString(dataReader["Navn"]);
                        indtægt.Børn.Adresse = Convert.ToString(dataReader["Adresse"]);
                        indtægt.Børn.Telefon = Convert.ToString(dataReader["Telefon"]);
                        indtægt.Børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);

                        indtægtlist.Add(indtægt);
                    }
                }
            }

            return indtægtlist;
        }



        public List<Indtægt> GetAntalSolgteLodseddelerASC()
        {
            List<Indtægt> indtægtlist = new List<Indtægt>();
            string sql = "Select Indtægt.Indtægt_ID, Salg.Salg_ID, Salg.Dato, Børn.Børn_ID, Børnegruppe.Børnegruppe_ID, Børnegruppe.Gruppenavn,  Børn.Navn, Børn.Adresse, Børn.Telefon, Børn.AntalSolgteLodseddeler, Børnegruppe.AntalSolgteLodSeddeler  FROM Indtægt\r\njoin Salg on Indtægt.Salg_ID = Salg.Salg_ID\r\njoin Børn on Salg.Salg_ID = Børn.Børn_ID\r\njoin Børnegruppe on Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID\r\nORDER BY Børn.AntalSolgteLodseddeler ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Indtægt indtægt = new Indtægt();
                        indtægt.Indtægt_ID = Convert.ToInt32(dataReader["Indtægt_ID"]);
                        indtægt.Salg_ID = Convert.ToInt32(dataReader["Salg_ID"]);


                        indtægt.Salg = new Salg();
                        indtægt.Børn = new Børn();
                        indtægt.Børnegruppe = new Børnegruppe();


                        indtægt.Salg.Dato = (DateTime)(dataReader["Dato"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["Dato"]));
                        indtægt.Børn.Børn_ID = Convert.ToInt32(dataReader["Børn_ID"]);
                        indtægt.Børnegruppe.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);
                        indtægt.Børnegruppe.Gruppenavn = Convert.ToString(dataReader["Gruppenavn"]);
                        indtægt.Børn.Navn = Convert.ToString(dataReader["Navn"]);
                        indtægt.Børn.Adresse = Convert.ToString(dataReader["Adresse"]);
                        indtægt.Børn.Telefon = Convert.ToString(dataReader["Telefon"]);
                        indtægt.Børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);

                        indtægtlist.Add(indtægt);
                    }
                }
            }

            return indtægtlist;
        }

    }
}
