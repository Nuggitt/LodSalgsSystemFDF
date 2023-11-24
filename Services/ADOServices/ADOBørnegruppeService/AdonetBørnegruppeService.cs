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

        public Børnegruppe GetBørnegruppeById(int id)
        {
            List<Børnegruppe> børnegruppeList = new List<Børnegruppe>();
            Børnegruppe børnegruppe = new Børnegruppe();
            string sql = "SELECT * Børnegruppe WHERE Børnegruppe_ID = @Børnegruppe_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Børnegruppe_ID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        børnegruppe.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);
                        børnegruppe.Gruppenavn = Convert.ToString(reader["Gruppenavn"]);
                        børnegruppe.Lokale = Convert.ToString(reader["Lokale"]);
                        børnegruppe.Antalbørn = Convert.ToInt32(reader["AntalBørn"]);
                        børnegruppe.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                        børnegruppe.AntalLodSeddelerPrGruppe = Convert.ToInt32(reader["AntalLodSeddelerPrGruppe"]);
                        børnegruppe.AntalSolgteLodSeddeler = Convert.ToInt32(reader["AntalSolgteLodSeddeler"]);
                        børnegruppeList.Add(børnegruppe);
                        
                    }
                }
            }
            return børnegruppe;
        }

        public Børnegruppe CreateBørnegruppe(Børnegruppe børnegruppe )
        {
            List<Børnegruppe> børnegruppelist = new List<Børnegruppe>();
            string sql = "INSERT INTO Børnegruppe (Børnegruppe_ID, Gruppenavn, Lokale, AntalBørn, AntalLodseddelerPrGruppe, AntalSolgteLodSeddeler) VALUES(@Børnegruppe_ID, @Gruppenavn, @Lokale, @AntalBørn, @AntalLodseddelerPrGruppe, @AntalSolgteLodSeddeler)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Børnegruppe_ID",børnegruppe.Børnegruppe_ID );
                    command.Parameters.AddWithValue("@Gruppenavn", børnegruppe.Gruppenavn);
                    command.Parameters.AddWithValue("@Lokale", børnegruppe.Lokale);
                    command.Parameters.AddWithValue("@AntalBørn", børnegruppe.Antalbørn);
                    command.Parameters.AddWithValue("@AntalLodseddelerPrGruppe", børnegruppe.AntalLodSeddelerPrGruppe);
                    command.Parameters.AddWithValue("@AntalSolgteLodSeddeler", børnegruppe.AntalSolgteLodSeddeler);

                    børnegruppelist.Add(børnegruppe);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return børnegruppe;
        }

        public Børnegruppe DeleteBørnegruppe(Børnegruppe børnegruppe)
        {
            List<Børnegruppe> børnelist = new List<Børnegruppe>();
            string sql = "DELETE FROM Børnegruppe WHERE Børnegruppe_ID = @Børnegruppe_ID";

            using SqlConnection connection = new SqlConnection(connectionString);

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Børnegruppe_ID", børnegruppe.Børnegruppe_ID);

                int numberOfRowsAffected = command.ExecuteNonQuery();
            }
            return børnegruppe;
        }

        public Børnegruppe UpdateBørnegruppe(Børnegruppe børnegruppe)
        {
            string sql = "UPDATE Børnegruppe  Gruppenavn = @Gruppenavn, Lokale = @Lokale, AntalBørn = @AntalBørn, AntalLodseddelerPrGruppe = @AntalLodseddelerPrGruppe, AntalSolgteLodSeddeler = @AntalSolgteLodSeddeler WHERE Børnegruppe_ID = Børnegruppe_ID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Børnegruppe_ID", børnegruppe.Børnegruppe_ID);
                    command.Parameters.AddWithValue("@Gruppenavn", børnegruppe.Gruppenavn);
                    command.Parameters.AddWithValue("@Lokale", børnegruppe.Lokale);
                    command.Parameters.AddWithValue("@AntalBørn", børnegruppe.Antalbørn);
                    command.Parameters.AddWithValue("@AntalLodseddelerPrGruppe", børnegruppe.AntalLodSeddelerPrGruppe);
                    command.Parameters.AddWithValue("@AntalSolgteLodSeddeler", børnegruppe.AntalSolgteLodSeddeler);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return børnegruppe;
        }
    }
}
