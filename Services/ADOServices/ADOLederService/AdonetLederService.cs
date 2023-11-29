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
                        leder.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);
                        
                        lederList.Add(leder);


                    }
                }
            }
            return lederList;
        }

        public Leder CreateLeder(Leder leder)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertSql = "INSERT INTO dbo.Leder (Leder_ID, Navn, Adresse, Telefon, Email, ErLotteriBestyrer,  Børnegruppe_ID) VALUES(@Leder_ID, @Navn, @Adresse, @Telefon, @Email, @ErLotteriBestyrer,  @Børnegruppe_ID)";

                        using (SqlCommand insertCommand = new SqlCommand(insertSql, connection, transaction))
                        {
                            insertCommand.Parameters.AddWithValue("@Leder_ID", leder.Leder_ID);
                            insertCommand.Parameters.AddWithValue("@Navn", leder.Navn);
                            insertCommand.Parameters.AddWithValue("@Adresse", leder.Leder_ID);
                            insertCommand.Parameters.AddWithValue("@Telefon", leder.Telefon);
                            insertCommand.Parameters.AddWithValue("@Email", leder.Email);
                            insertCommand.Parameters.AddWithValue("@ErLotteriBestyrer", leder.ErLotteriBestyrer);
                            insertCommand.Parameters.AddWithValue("@Børnegruppe_ID", leder.Børnegruppe_ID);

                            insertCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, log the error, or perform any necessary cleanup.
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return leder;
        }
        public Leder GetLederByID(int Leder_ID)
        {
            List<Leder> lederList = new List<Leder>();
            Leder leder = new Leder();
            string sql = "SELECT * FROM dbo.Leder WHERE Leder_ID = @Leder_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Leder_ID", Leder_ID);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        leder.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                        leder.Navn = Convert.ToString(reader["Navn"]);
                        leder.Adresse = Convert.ToString(reader["Adresse"]);
                        leder.Telefon = Convert.ToString(reader["Telefon"]);
                        leder.Email = Convert.ToString(reader["Email"]);
                        leder.ErLotteriBestyrer = Convert.ToBoolean(reader["ErLotteriBestyrer"]);
                        leder.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);

                        lederList.Add(leder);
                    }
                }
            }
            return leder;
        }
        public Leder DeleteLeder(Leder leder)
        {
            List<Leder> lederList = new List<Leder>();
            string sql = "DELETE FROM dbo.Leder WHERE Leder_ID = @Leder_ID";

            using SqlConnection connection = new SqlConnection(connectionString);

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Leder_ID", leder.Leder_ID);

                int numberOfRowsAffected = command.ExecuteNonQuery();
            }
            return leder;
        }
        public Leder UpdateLeder(Leder leder)
        {
            string sql = "UPDATE Leder Leder_ID = @Leder_ID, Navn = @Navn, Adresse = @Adresse, Telefon = @Telefon, Email = @Email, ErLotteriBestyrer = @ErLotteriBestyrer, Børnegruppe_ID = @Børnegruppe_ID WHERE Leder_ID = @Leder_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Leder_ID", leder.Leder_ID);
                    command.Parameters.AddWithValue("@Navn", leder.Navn);
                    command.Parameters.AddWithValue("@Adresse", leder.Adresse);
                    command.Parameters.AddWithValue("@Telefon", leder.Telefon);
                    command.Parameters.AddWithValue("@Email", leder.Email);
                    command.Parameters.AddWithValue("@ErLotteriBestyrer", leder.ErLotteriBestyrer);
                    command.Parameters.AddWithValue("@Børnegruppe_ID", leder.Børnegruppe_ID);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return leder;
        }
    }
}
