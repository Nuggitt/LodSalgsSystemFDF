using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOSalgService
{
    public class AdonetSalgService
    {
        private IConfiguration configuration { get; }
        string connectionString;

        public AdonetSalgService() { }

        public AdonetSalgService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("Datacraft.dk");
        }

        public List<Salg> GetAllSalgs()
        {
            List<Salg> salgList = new List<Salg>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Salg_ID, Børn.Børn_ID, Børn.Navn, Børn.Telefon, Børnegruppe.Gruppenavn, Leder.Navn, Salg.Dato, Salg.AntalLodseddelerRetur, Salg.AntalSolgteLodseddelerPrSalg, Børn.AntalSolgteLodseddeler, Salg.Pris\r\nFROM dbo.Salg\r\nJoin Børnegruppe on Børnegruppe.Børnegruppe_ID = Salg.Børnegruppe_ID\r\nJoin Børn on Børn.Børn_ID = Salg.Børn_ID\r\nJoin Leder on Leder.Leder_ID = Salg.Leder_ID";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Salg salg = new Salg();
                        salg.Leder = new Leder();
                        salg.Børn = new Børn();
                        salg.Børnegruppe = new Børnegruppe();

                        salg.Salg_ID = reader.GetInt32(0);
                        salg.Børn_ID = reader.GetInt32(1);
                        salg.Børn.Navn = reader.GetString(2);
                        salg.Børn.Telefon = reader.GetString(3);
                        salg.Børnegruppe.Gruppenavn = reader.GetString(4);
                        salg.Leder.Navn = reader.GetString(5);
                        salg.Dato = reader.GetDateTime(6);
                        salg.AntalLodseddelerRetur = reader.GetInt32(7);
                        salg.AntalSolgteLodseddelerPrSalg = reader.GetInt32(8);
                        salg.Børn.AntalSolgteLodseddeler = reader.GetInt32(9);
                        salg.Pris = reader.GetDouble(10);
                        salgList.Add(salg);


                    }
                }
            }
            return salgList;
        }

        public Salg GetSalgById(int id)
        {
            List<Salg> salgsList = new List<Salg>();
            Salg salg = new Salg();
            string sql = "SELECT * FROM dbo.Salg WHERE Salg_ID = @Salg_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Salg_ID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salg.Salg_ID = Convert.ToInt32(reader["Salg_ID"]);
                        salg.Børn_ID = Convert.ToInt32(reader["Børn_ID"]);
                        salg.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);
                        salg.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                        salg.Dato = Convert.ToDateTime(reader["Dato"]);
                        salg.AntalLodseddelerRetur = Convert.ToInt32(reader["AntalLodseddelerRetur"]);
                        salg.AntalSolgteLodseddelerPrSalg = Convert.ToInt32(reader["AntalSolgteLodseddelerPrSalg"]);
                        salg.Pris = Convert.ToDouble(reader["Pris"]);
                        salgsList.Add(salg);
                    }
                }
            }
            return salg;
        }

        public Salg CreateSalg(Salg salg)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertSql = "INSERT INTO dbo.Salg (Børn_ID, Børnegruppe_ID, Leder_ID, Dato, AntalLodseddelerRetur, AntalSolgteLodseddelerPrSalg,  Pris) VALUES(@Børn_ID, @Børnegruppe_ID, @Leder_ID, @Dato, @AntalLodseddelerRetur, @AntalSolgteLodSeddelerPrSalg,  @Pris);" +
                         "SELECT SCOPE_IDENTITY();";

                        using (SqlCommand insertCommand = new SqlCommand(insertSql, connection, transaction))
                        {

                            insertCommand.Parameters.AddWithValue("@Børn_ID", salg.Børn_ID);
                            insertCommand.Parameters.AddWithValue("@Børnegruppe_ID", salg.Børnegruppe_ID);
                            insertCommand.Parameters.AddWithValue("@Leder_ID", salg.Leder_ID);
                            insertCommand.Parameters.AddWithValue("@Dato", salg.Dato);
                            insertCommand.Parameters.AddWithValue("@AntalLodseddelerRetur", salg.AntalLodseddelerRetur);
                            insertCommand.Parameters.AddWithValue("@AntalSolgteLodseddelerPrSalg", salg.AntalSolgteLodseddelerPrSalg);
                            insertCommand.Parameters.AddWithValue("@Pris", salg.Pris);

                            //insertCommand.ExecuteNonQuery();
                            salg.Salg_ID = Convert.ToInt32(insertCommand.ExecuteScalar());

                        }

                        //if (salg.AntalLodseddelerRetur > 0)
                        //{
                        //    string sqlbørnegruppe = "Update dbo.Børnegruppe SET AntalLodSeddelerPrGruppe = AntalLodSeddelerPrGruppe + @AntalLodSeddelerPrGruppe WHERE Børnegruppe_ID = @Børnegruppe_ID";

                        //    using (SqlCommand updcommand = new SqlCommand(sqlbørnegruppe, connection, transaction))
                        //    {
                        //        updcommand.Parameters.AddWithValue("@Børnegruppe_ID", salg.Børnegruppe_ID);
                        //        updcommand.Parameters.AddWithValue("@AntalLodSeddelerPrGruppe", salg.AntalLodseddelerRetur);

                        //        updcommand.ExecuteNonQuery();
                        //    }


                        //}

                        if (salg.AntalSolgteLodseddelerPrSalg != null)
                        {
                            string Sqlbørn = "UPDATE dbo.Børn SET AntalSolgteLodseddeler = AntalSolgteLodseddeler + @AntalSolgteLodseddeler WHERE Børn_ID = @Børn_ID";

                            using (SqlCommand updateCommand = new SqlCommand(Sqlbørn, connection, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@Børn_ID", salg.Børn_ID);
                                updateCommand.Parameters.AddWithValue("@AntalSolgteLodseddeler", salg.AntalSolgteLodseddelerPrSalg);

                                updateCommand.ExecuteNonQuery();
                            }

                            string sqlbørn = "Update dbo.Børn SET GivetLodsedler = GivetLodsedler - @GivetLodsedler WHERE Børn_ID = @Børn_ID";

                            using (SqlCommand updcommand = new SqlCommand(sqlbørn, connection, transaction))
                            {
                                updcommand.Parameters.AddWithValue("@Børn_ID", salg.Børn_ID);
                                updcommand.Parameters.AddWithValue("@GivetLodsedler", salg.AntalSolgteLodseddelerPrSalg);

                                updcommand.ExecuteNonQuery();
                            }

                            string sqlbørnegruppe = "UPDATE dbo.Børnegruppe SET AntalSolgteLodSeddelerPrGruppe = AntalSolgteLodSeddelerPrGruppe + @AntalSolgteLodSeddelerPrGruppe WHERE Børnegruppe_ID = @Børnegruppe_ID";

                            using (SqlCommand updcommand = new SqlCommand(sqlbørnegruppe, connection, transaction))
                            {
                                updcommand.Parameters.AddWithValue("@Børnegruppe_ID", salg.Børnegruppe_ID);
                                updcommand.Parameters.AddWithValue("@AntalSolgteLodSeddelerPrGruppe", salg.AntalSolgteLodseddelerPrSalg);

                                updcommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return salg;
        }


        public Salg DeleteSalg(Salg salg)
        {
            List<Salg> salgsList = new List<Salg>();
            string sql = "DELETE FROM dbo.Salg WHERE Salg_ID = @Salg_ID";

            using SqlConnection connection = new SqlConnection(connectionString);

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Salg_Id", salg.Salg_ID);

                int numberOfRowsAffected = command.ExecuteNonQuery();
            }
            return salg;
        }

        public Salg UpdateSalg(Salg salg)
        {
            string sql = "UPDATE dbo.Salg Set Børn_ID = @Børn_ID, Børnegruppe_ID = @Børnegruppe_ID, Leder_ID = @Leder_ID, Dato = @Dato, AntalLodseddelerRetur = @AntalLodseddelerRetur, AntalSolgteLodSeddelerPrSalg = @AntalSolgteLodSeddelerPrSalg,   Pris = @Pris WHERE Salg_ID = @Salg_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Salg_ID", salg.Salg_ID);
                    command.Parameters.AddWithValue("@Børn_ID", salg.Børn_ID);
                    command.Parameters.AddWithValue("@Børnegruppe_ID", salg.Børnegruppe_ID);
                    command.Parameters.AddWithValue("@Leder_ID", salg.Leder_ID);
                    command.Parameters.AddWithValue("@Dato", salg.Dato);
                    command.Parameters.AddWithValue("@AntalLodseddelerRetur", salg.AntalLodseddelerRetur);
                    command.Parameters.AddWithValue("@AntalSolgteLodseddelerPrSalg", salg.AntalSolgteLodseddelerPrSalg);
                    command.Parameters.AddWithValue("@Pris", salg.Pris);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return salg;
        }
        public List<Salg> GetBørnegruppeByID(int ID)
        {
            List<Salg> listsalg = new List<Salg>();
            Salg salg = new Salg();
            string sql = "Select * from Salg Where Børnegruppe_ID = @Børnegruppe_ID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Børnegruppe_ID", ID);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        salg.Salg_ID = Convert.ToInt32(reader["Salg_ID"]);
                        salg.Børn_ID = Convert.ToInt32(reader["Børn_ID"]);
                        salg.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);
                        salg.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                        salg.Dato = Convert.ToDateTime(reader["Dato"]);
                        salg.AntalLodseddelerRetur = Convert.ToInt32(reader["AntalLodseddelerRetur"]);
                        salg.AntalSolgteLodseddelerPrSalg = Convert.ToInt32(reader["AntalSolgteLodseddelerPrSalg"]);
                        salg.Pris = Convert.ToDouble(reader["Pris"]);
                        listsalg.Add(salg);

                    }
                }

            }
            return listsalg;
        }

        public IEnumerable<Salg> PriceFilters(float maxPrice, float minPrice)
        {
            if (minPrice < 0 || maxPrice < 0)
            {
                throw new NegativeAmountExceptioncs("MinPrice and MaxPrice cannot be negative values.");
            }
            List<Salg> filterList = new List<Salg>();

            string sql = "SELECT * FROM Salg WHERE (@MinPrice = 0 OR Pris >= @MinPrice) AND (@MaxPrice = 0 OR Pris <= @MaxPrice)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@MinPrice", minPrice);
                command.Parameters.AddWithValue("@MaxPrice", maxPrice);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Salg salg = new Salg();
                        salg.Salg_ID = Convert.ToInt32(reader["Salg_ID"]);
                        salg.Børn_ID = Convert.ToInt32(reader["Børn_ID"]);
                        salg.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);
                        salg.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                        salg.Dato = Convert.ToDateTime(reader["Dato"]);
                        salg.AntalLodseddelerRetur = Convert.ToInt32(reader["AntalLodseddelerRetur"]);
                        salg.AntalSolgteLodseddelerPrSalg = Convert.ToInt32(reader["AntalSolgteLodseddelerPrSalg"]);
                        salg.Pris = Convert.ToDouble(reader["Pris"]);

                        filterList.Add(salg);
                    }

                    return filterList;
                }
            }

        }

        public IEnumerable<Salg> GetBørnById(int id, int bid)
        {
            List<Salg> listsalg = new List<Salg>();
            Salg salg = new Salg();
            string sql = "Select * From dbo.Børn WHERE Børn_ID = @Børn_ID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Børn_ID", id);
                command.Parameters.AddWithValue("@Børnegruppe_ID", bid);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        salg.Børn_ID = Convert.ToInt32(reader["Børn_ID"]);
                        salg.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);

                        listsalg.Add(salg);

                    }
                }

            }
            return listsalg;
        }

        public List<Leder> GetLederOptions()
        {
            List<Leder> lederOptions = new List<Leder>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT Leder_ID, Navn FROM Leder ORDER BY Navn";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Leder leder = new Leder();
                            {
                                leder.Leder_ID = reader.GetInt32(0);
                                leder.Navn = reader.GetString(1);
                            };

                            lederOptions.Add(leder);
                        }
                    }
                }
            }

            return lederOptions;
        }

        public List<Salg> GetAntalSolgteLodseddelerDESC()
        {
            List<Salg> salgList = new List<Salg>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Salg_ID, Børn.Børn_ID, Børn.Navn, Børn.Telefon, Børnegruppe.Gruppenavn, Leder.Navn, Salg.Dato, Salg.AntalLodseddelerRetur, Salg.AntalSolgteLodseddelerPrSalg, Børn.AntalSolgteLodseddeler, Salg.Pris\r\nFROM dbo.Salg\r\nJoin Børnegruppe on Børnegruppe.Børnegruppe_ID = Salg.Børnegruppe_ID\r\nJoin Børn on Børn.Børn_ID = Salg.Børn_ID\r\nJoin Leder on Leder.Leder_ID = Salg.Leder_ID ORDER BY Børn.AntalSolgteLodseddeler DESC";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Salg salg = new Salg();
                        salg.Leder = new Leder();
                        salg.Børn = new Børn();
                        salg.Børnegruppe = new Børnegruppe();

                        salg.Salg_ID = reader.GetInt32(0);
                        salg.Børn_ID = reader.GetInt32(1);
                        salg.Børn.Navn = reader.GetString(2);
                        salg.Børn.Telefon = reader.GetString(3);
                        salg.Børnegruppe.Gruppenavn = reader.GetString(4);
                        salg.Leder.Navn = reader.GetString(5);
                        salg.Dato = reader.GetDateTime(6);
                        salg.AntalLodseddelerRetur = reader.GetInt32(7);
                        salg.AntalSolgteLodseddelerPrSalg = reader.GetInt32(8);
                        salg.Børn.AntalSolgteLodseddeler = reader.GetInt32(9);
                        salg.Pris = reader.GetDouble(10);
                        salgList.Add(salg);


                    }
                }
            }
            return salgList;
        }

        public List<Salg> GetAntalSolgteLodseddelerASC()
        {
            List<Salg> salgList = new List<Salg>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Salg_ID, Børn.Børn_ID, Børn.Navn, Børn.Telefon, Børnegruppe.Gruppenavn, Leder.Navn, Salg.Dato, Salg.AntalLodseddelerRetur, Salg.AntalSolgteLodseddelerPrSalg, Børn.AntalSolgteLodseddeler, Salg.Pris\r\nFROM dbo.Salg\r\nJoin Børnegruppe on Børnegruppe.Børnegruppe_ID = Salg.Børnegruppe_ID\r\nJoin Børn on Børn.Børn_ID = Salg.Børn_ID\r\nJoin Leder on Leder.Leder_ID = Salg.Leder_ID ORDER BY Børn.AntalSolgteLodseddeler ASC";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Salg salg = new Salg();
                        salg.Leder = new Leder();
                        salg.Børn = new Børn();
                        salg.Børnegruppe = new Børnegruppe();

                        salg.Salg_ID = reader.GetInt32(0);
                        salg.Børn_ID = reader.GetInt32(1);
                        salg.Børn.Navn = reader.GetString(2);
                        salg.Børn.Telefon = reader.GetString(3);
                        salg.Børnegruppe.Gruppenavn = reader.GetString(4);
                        salg.Leder.Navn = reader.GetString(5);
                        salg.Dato = reader.GetDateTime(6);
                        salg.AntalLodseddelerRetur = reader.GetInt32(7);
                        salg.AntalSolgteLodseddelerPrSalg = reader.GetInt32(8);
                        salg.Børn.AntalSolgteLodseddeler = reader.GetInt32(9);
                        salg.Pris = reader.GetDouble(10);
                        salgList.Add(salg);


                    }
                }
            }
            return salgList;
        }
    }
}
