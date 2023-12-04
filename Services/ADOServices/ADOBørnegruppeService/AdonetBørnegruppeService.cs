using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
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
        public async Task<List<Børnegruppe>> GetAllBørnegruppeAsync()
        {
            //IEnumerable<Børnegruppe> lstbørnegruppe = new IEnumerable<Børnegruppe>();
            List<Børnegruppe> lstbørnegruppe = new List<Børnegruppe>();
            string sql = "Select * from Børnegruppe";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (await dataReader.ReadAsync())
                    {
                        Børnegruppe børnegruppe = new Børnegruppe();
                        børnegruppe.Børnegruppe_ID = Convert.ToInt32(dataReader["Børnegruppe_ID"]);
                        børnegruppe.Gruppenavn = Convert.ToString(dataReader["Gruppenavn"]);
                        børnegruppe.Lokale = Convert.ToString(dataReader["Lokale"]);
                        børnegruppe.Antalbørn = Convert.ToInt32(dataReader["AntalBørn"]);
                        børnegruppe.Leder_ID = Convert.ToInt32(dataReader["Leder_ID"]);
                        børnegruppe.AntalLodSeddelerPrGruppe = Convert.ToInt32(dataReader["AntalLodSeddelerPrGruppe"]);
                        børnegruppe.AntalSolgteLodseddeler = Convert.ToInt32(dataReader["AntalSolgteLodseddeler"]);

                        lstbørnegruppe.Add(børnegruppe);
                    }
                }
            }
            return lstbørnegruppe;
        }

        public async  Task<Børnegruppe> GetBørnegruppeById(int id)
        {
            List<Børnegruppe> børnegruppeList = new List<Børnegruppe>();
            Børnegruppe børnegruppe = new Børnegruppe();
            string sql = "SELECT * FROM Børnegruppe WHERE Børnegruppe_ID = @Børnegruppe_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Børnegruppe_ID", id);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        //if (børnegruppe.Børnegruppe_ID < 0)
                        //{
                        //    try
                        //    {

                        //    }
                        //}
                        børnegruppe.Børnegruppe_ID = Convert.ToInt32(reader["Børnegruppe_ID"]);
                        børnegruppe.Gruppenavn = Convert.ToString(reader["Gruppenavn"]);
                        børnegruppe.Lokale = Convert.ToString(reader["Lokale"]);
                        børnegruppe.Antalbørn = Convert.ToInt32(reader["AntalBørn"]);
                        børnegruppe.Leder_ID = Convert.ToInt32(reader["Leder_ID"]);
                        børnegruppe.AntalLodSeddelerPrGruppe = Convert.ToInt32(reader["AntalLodSeddelerPrGruppe"]);
                        børnegruppe.AntalSolgteLodseddeler = Convert.ToInt32(reader["AntalSolgteLodSeddeler"]);
                        børnegruppeList.Add(børnegruppe);
                        
                    }
                }
            }
            return børnegruppe;
        }

        public Børnegruppe CreateBørnegruppe(Børnegruppe børnegruppe )
        {
            if(børnegruppe.Børnegruppe_ID <= 0)
            {
                throw new NegativeAmountExceptioncs("Børnegruppe_ID må ikke være negativt eller nul");
            }

            if (CheckIfBørnegruppeIdExists(børnegruppe.Børnegruppe_ID.ToString()))
            {
                throw new DuplicateKeyException("Børnegruppe ID Eksisteres allerede, brug en anden.");
            }

            List<Børnegruppe> børnegruppelist = new List<Børnegruppe>();
            string sql = "INSERT INTO dbo.Børnegruppe (Børnegruppe_ID, Gruppenavn, Lokale, AntalBørn, Leder_ID, AntalLodseddelerPrGruppe, AntalSolgteLodseddeler) VALUES(@Børnegruppe_ID, @Gruppenavn, @Lokale, @AntalBørn, @Leder_ID, @AntalLodseddelerPrGruppe, @AntalSolgteLodseddeler)";
           
            
            //virker ikke lige NU
            //foreach (Børnegruppe duplicatekey in børnegruppelist)
            //{
            //    if (duplicatekey.Børnegruppe_ID == børnegruppe.Børnegruppe_ID)
            //    {
            //        throw new DuplicateKeyException(" ID Eksiterer allerede");
            //    }
            //}
                

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    
                    command.Parameters.AddWithValue("@Børnegruppe_ID",børnegruppe.Børnegruppe_ID );
                    command.Parameters.AddWithValue("@Gruppenavn", børnegruppe.Gruppenavn);
                    command.Parameters.AddWithValue("@Lokale", børnegruppe.Lokale);
                    command.Parameters.AddWithValue("@AntalBørn", børnegruppe.Antalbørn);
                    command.Parameters.AddWithValue("@Leder_ID", børnegruppe.Leder_ID);
                    command.Parameters.AddWithValue("@AntalLodseddelerPrGruppe", børnegruppe.AntalLodSeddelerPrGruppe);
                    command.Parameters.AddWithValue("@AntalSolgteLodSeddeler", børnegruppe.AntalSolgteLodseddeler);

                    foreach (Børnegruppe duplicatekey in børnegruppelist)
                    {
                        if (duplicatekey.Børnegruppe_ID == børnegruppe.Børnegruppe_ID)
                        {
                            throw new DuplicateKeyException(" ID Eksiterer allerede");
                        }
                    }
                    børnegruppelist.Add(børnegruppe);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return børnegruppe;
        }
        // tjekker om Børnegruppe_ID eksiteres, bliver brugt i CreateBØrnegruppe for exception hvis ID allerede eksiterer
        public bool CheckIfBørnegruppeIdExists(string børnegruppeId)
        {
            string sql = "SELECT COUNT(*) FROM Børnegruppe WHERE Børnegruppe_ID = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", børnegruppeId);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
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
            string sql = "UPDATE dbo.Børnegruppe SET Gruppenavn = @Gruppenavn, Lokale = @Lokale, AntalBørn = @AntalBørn, AntalLodSeddelerPrGruppe = @AntalLodSeddelerPrGruppe, AntalSolgteLodseddeler = @AntalSolgteLodseddeler WHERE Børnegruppe_ID = @Børnegruppe_ID";

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
                    command.Parameters.AddWithValue("@AntalSolgteLodseddeler", børnegruppe.AntalSolgteLodseddeler);

                    int numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            return børnegruppe;
        }
        public List<Børnegruppe> GetBørnegruppeByName(string name)
        {
            List<Børnegruppe> lstbørnegruppe = new List<Børnegruppe>();
            Børnegruppe børnegruppe = new Børnegruppe();
            string sql = "Select * from Børnegruppe Where Gruppenavn like @Gruppenavn";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Gruppenavn", name);
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
                        børnegruppe.AntalSolgteLodseddeler = Convert.ToInt32(reader["AntalSolgteLodSeddeler"]);
                        lstbørnegruppe.Add(børnegruppe);
                    }
                }
                
            }
            return lstbørnegruppe;
        } 

    }
}
