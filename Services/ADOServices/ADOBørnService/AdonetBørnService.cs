using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;

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

        public async Task<List<Børn>> GetAllBørn()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "Select * from Børn";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (await dataReader.ReadAsync())
                    {
                        Børn børn = new Børn();
                        børn.Børn_ID = Convert.ToInt32(dataReader["Børn_ID"]);
                        børn.Navn = Convert.ToString(dataReader["Navn"]);
                        børn.Adresse = Convert.ToString(dataReader["Adresse"]);
                        børn.Telefon = Convert.ToString(dataReader["Telefon"]);
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);


                    }

                }

            }

            return listbørn;
        }

        public async Task<Børn> GetBørn(int id)
        {
            List<Børn> listbørn = new List<Børn>();
            Børn børn = new Børn();
            string sql = "Select * FROM dbo.Børn WHERE Børn_ID = @Børn_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Børn_ID", id);

                await connection.OpenAsync();

                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (await dataReader.ReadAsync())

                    {
                        børn.Børn_ID = Convert.ToInt32(dataReader["Børn_ID"]);
                        børn.Navn = Convert.ToString(dataReader["Navn"]);
                        børn.Adresse = Convert.ToString(dataReader["Adresse"]);
                        børn.Telefon = Convert.ToString(dataReader["Telefon"]);
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
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
            if (børn.Børn_ID <= 0 || børn.AntalSolgteLodseddeler < 0 || børn.Børnegruppe_ID <= 0)
            {
                throw new NegativeAmountExceptioncs("Værdi må ikke være negativt");
            }

            if (TjekIdEksisterer(børn.Børn_ID.ToString()))
            {
                throw new DuplicateKeyException(" ID Eksisterer allerede, brug en anden.");
            }

            List<Børn> listbørn = new List<Børn>();
            string sql = "INSERT INTO Børn (Børn_ID, Navn, Adresse, Telefon, GivetLodsedler, AntalSolgteLodseddeler, Børnegruppe_ID) VALUES(@Børn_ID, @Navn, @Adresse, @Telefon, @GivetLodsedler, @AntalSolgteLodseddeler, @Børnegruppe_ID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Børn_ID", børn.Børn_ID);
                    command.Parameters.AddWithValue("@Navn", børn.Navn);
                    command.Parameters.AddWithValue("@Adresse", børn.Adresse);
                    command.Parameters.AddWithValue("@Telefon", børn.Telefon);
                    command.Parameters.AddWithValue("@GivetLodsedler", børn.GivetLodsedler);
                    command.Parameters.AddWithValue("@AntalSolgteLodseddeler", børn.AntalSolgteLodseddeler);
                    command.Parameters.AddWithValue("@Børnegruppe_ID", børn.Børnegruppe_ID);

                    listbørn.Add(børn);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return børn;
        }

        public bool TjekIdEksisterer(string børnId)
        {
            string sql = "SELECT COUNT(*) FROM Børn WHERE Børn_ID = @Børn_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Børn_ID", børnId);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
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
            string sql = "UPDATE Børn SET Navn = @Navn, Adresse = @Adresse, Telefon = @Telefon, GivetLodsedler = @GivetLodsedler, AntalSolgteLodseddeler = @AntalSolgteLodseddeler, Børnegruppe_ID = @Børnegruppe_ID WHERE Børn_ID = @Børn_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Børn_ID", børn.Børn_ID);
                    command.Parameters.AddWithValue("@Navn", børn.Navn);
                    command.Parameters.AddWithValue("@Adresse", børn.Adresse);
                    command.Parameters.AddWithValue("@Telefon", børn.Telefon);
                    command.Parameters.AddWithValue("@GivetLodsedler", børn.GivetLodsedler);
                    command.Parameters.AddWithValue("@AntalSolgteLodseddeler", børn.AntalSolgteLodseddeler);
                    command.Parameters.AddWithValue("@Børnegruppe_ID", børn.Børnegruppe_ID);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return børn;
        }



        public List<Børn> GetAllBørnNavnDescending()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "SELECT * FROM Børn ORDER BY Navn DESC";

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
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }
            }

            return listbørn;
        }

        public List<Børn> GetAllBørnIDDescending()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "SELECT * FROM Børn ORDER BY Børn_ID DESC";

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
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }
            }

            return listbørn;
        }

        public List<Børn> GetAllBørnAntalSolgteLodseddelerDescending()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "SELECT * FROM Børn ORDER BY AntalSolgteLodseddeler DESC";

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
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }
            }

            return listbørn;
        }

        public List<Børn> GetAllBørnGruppeIDDescending()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "SELECT * FROM Børn ORDER BY Børnegruppe_ID DESC";

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
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }
            }

            return listbørn;
        }

        public List<Børn> GetAllBørnNavnAscending()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "SELECT * FROM Børn ORDER BY Navn ASC";

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
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }
            }

            return listbørn;
        }

        public List<Børn> GetAllBørnIDAscending()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "SELECT * FROM Børn ORDER BY Børn_ID ASC";

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
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }
            }

            return listbørn;
        }

        public List<Børn> GetAllBørnAntalSolgteLodseddelerAscending()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "SELECT * FROM Børn ORDER BY AntalSolgteLodseddeler ASC";

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
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }
            }

            return listbørn;
        }

        public List<Børn> GetAllBørnGruppeIDAscending()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "SELECT * FROM Børn ORDER BY Børnegruppe_ID ASC";

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
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }
            }

            return listbørn;
        }
        public List<Børn> GetBørnByName(string name)
        {
            List<Børn> listbørn = new List<Børn>();
            Børn børn = new Børn();
            string sql = "Select * from Børne Where Navn like @Navn";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Navn", name);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        børn.Børn_ID = Convert.ToInt32(reader["Børn_ID"]);
                        børn.Navn = Convert.ToString(reader["Navn"]);
                        børn.Adresse = Convert.ToString(reader["Adresse"]);
                        børn.Telefon = Convert.ToString(reader["Telefon"]);
                        børn.GivetLodsedler = Convert.ToInt32(reader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(reader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);                       
                        listbørn.Add(børn);
                    }
                }

        public List<Børn> GetGivetLodsedlerDescending()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "SELECT * FROM Børn ORDER BY GivetLodsedler DESC";

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
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }
            }

            return listbørn;
        }

        public List<Børn> GetGivetLodsedlerAscending()
        {
            List<Børn> listbørn = new List<Børn>();
            string sql = "SELECT * FROM Børn ORDER BY GivetLodsedler ASC";

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
                        børn.GivetLodsedler = Convert.ToInt32(dataReader["GivetLodsedler"]);
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);

                        listbørn.Add(børn);
                    }
                }
            }

            return listbørn;
        }
        public List<Børn> GetBørnByName(string name)
        {
            List<Børn> listbørn = new List<Børn>();
            Børn børn = new Børn();
            string sql = "Select * from Børne Where Navn like @Navn";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Navn", name);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        børn.Børn_ID = Convert.ToInt32(reader["Børn_ID"]);
                        børn.Navn = Convert.ToString(reader["Navn"]);
                        børn.Adresse = Convert.ToString(reader["Adresse"]);
                        børn.Telefon = Convert.ToString(reader["Telefon"]);                       
                        børn.AntalSolgteLodseddeler = Convert.ToInt32(reader["AntalSolgteLodseddeler"]);
                        børn.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);                       
                        listbørn.Add(børn);
                    }
                }

            }
            return listbørn;
            //public List<T> GetAllBørnItems<T>(string Børn, string Navn)
            //{
            //    List<T> listbørn = new List<T>();
            //    string sql = $"SELECT * FROM Børn ORDER BY Navn DESC";

            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        SqlCommand command = new SqlCommand(sql, connection);
            //        using (SqlDataReader dataReader = command.ExecuteReader())
            //        {
            //            while (dataReader.Read())
            //            {
            //                var børn = Activator.CreateInstance<T>();

            //                foreach (var property in typeof(T).GetProperties())
            //                {
            //                    if (!dataReader.IsDBNull(dataReader.GetOrdinal(property.Name)))
            //                    {
            //                        var value = dataReader[property.Name];
            //                        property.SetValue(børn, value == DBNull.Value ? null : value);
            //                    }
            //                }

            //                listbørn.Add(børn);
            //            }
            //        }
            //    }

            //    return listbørn;
            //}

        }
    }


}

