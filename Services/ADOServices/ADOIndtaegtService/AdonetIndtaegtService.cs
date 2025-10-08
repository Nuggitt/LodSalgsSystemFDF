using LodSalgsSystemFDF.Models;
using Microsoft.Data.Sqlite;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOIndtaegtService
{
    public class AdonetIndtaegtService
    {
        private IConfiguration configuration { get; }
        private readonly string connectionString;

        public AdonetIndtaegtService() { }

        public AdonetIndtaegtService(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // -------------------------------------------------------
        // Helpers
        private static DateTime? ReadNullableDate(SqliteDataReader r, string col)
        {
            var ord = r.GetOrdinal(col);
            return r.IsDBNull(ord) ? (DateTime?)null : r.GetDateTime(ord);
        }
        // -------------------------------------------------------

        public List<Indtaegt> GetAllIndtaegter()
        {
            var list = new List<Indtaegt>();
            // Alias’er harmoniserer kolonnenavne i resultatsættet
            const string sql = @"
SELECT
    Indtaegt.Indtaegt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Born.Born_ID,
    Bornegruppe.Bornegruppe_ID,
    Bornegruppe.Gruppenavn,
    Born.Navn,
    Born.Adresse,
    Born.Telefon,
    Born.AntalSolgteLodseddeler,
    Bornegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtaegt
JOIN Salg        ON Indtaegt.Salg_ID = Salg.Salg_ID
JOIN Born        ON Salg.Born_ID    = Born.Born_ID
JOIN Bornegruppe ON Bornegruppe.Bornegruppe_ID = Born.Bornegruppe_ID;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtaegt
                {
                    Indtaegt_ID = r.GetInt32(r.GetOrdinal("Indtaegt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Born = new Born
                    {
                        Born_ID = r.GetInt32(r.GetOrdinal("Born_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Bornegruppe = new Bornegruppe
                    {
                        Bornegruppe_ID = r.GetInt32(r.GetOrdinal("Bornegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public Indtaegt GetIndtaegtById(int id)
        {
            var ind = new Indtaegt();
            const string sql = "SELECT * FROM Indtaegt WHERE Indtaegt_ID = @Indtaegt_ID";
            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Indtaegt_ID", id);
            using var r = cmd.ExecuteReader();
            if (r.Read())
            {
                ind.Indtaegt_ID = r.GetInt32(r.GetOrdinal("Indtaegt_ID"));
                ind.Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID"));
            }
            return ind;
        }

        public Indtaegt CreateIndtaegt(Indtaegt indtaegt)
        {
            // NB: ingen schema i SQLite, og kolonner må ikke være kvalificeret med tabelnavn
            const string sql = "INSERT INTO Indtaegt (Indtaegt_ID, Salg_ID) VALUES (@Indtaegt_ID, @Salg_ID);";
            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Indtaegt_ID", indtaegt.Indtaegt_ID);
            cmd.Parameters.AddWithValue("@Salg_ID", indtaegt.Salg?.Salg_ID ?? indtaegt.Salg_ID);
            cmd.ExecuteNonQuery();
            return indtaegt;
        }

        public Indtaegt DeleteIndtaegt(Indtaegt indtaegt)
        {
            const string sql = "DELETE FROM Indtaegt WHERE Indtaegt_ID = @Indtaegt_ID";
            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Indtaegt_ID", indtaegt.Indtaegt_ID);
            cmd.ExecuteNonQuery();
            return indtaegt;
        }

        public Indtaegt UpdateIndtaegt(Indtaegt indtaegt)
        {
            const string sql = "UPDATE Indtaegt SET Salg_ID = @Salg_ID WHERE Indtaegt_ID = @Indtaegt_ID";
            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Indtaegt_ID", indtaegt.Indtaegt_ID);
            cmd.Parameters.AddWithValue("@Salg_ID", indtaegt.Salg_ID);
            cmd.ExecuteNonQuery();
            return indtaegt;
        }

        public List<Indtaegt> GetIndtaegtIDDESC()
        {
            var list = new List<Indtaegt>();
            const string sql = @"
SELECT
    Indtaegt.Indtaegt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Born.Born_ID,
    Bornegruppe.Bornegruppe_ID,
    Bornegruppe.Gruppenavn,
    Born.Navn,
    Born.Adresse,
    Born.Telefon,
    Born.AntalSolgteLodseddeler,
    Bornegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtaegt
JOIN Salg        ON Indtaegt.Salg_ID = Salg.Salg_ID
JOIN Born        ON Salg.Born_ID    = Born.Born_ID
JOIN Bornegruppe ON Bornegruppe.Bornegruppe_ID = Born.Bornegruppe_ID
ORDER BY Indtaegt.Indtaegt_ID DESC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtaegt
                {
                    Indtaegt_ID = r.GetInt32(r.GetOrdinal("Indtaegt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Born = new Born
                    {
                        Born_ID = r.GetInt32(r.GetOrdinal("Born_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Bornegruppe = new Bornegruppe
                    {
                        Bornegruppe_ID = r.GetInt32(r.GetOrdinal("Bornegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public List<Indtaegt> GetIndtaegtIDASC()
        {
            var list = new List<Indtaegt>();
            const string sql = @"
SELECT
    Indtaegt.Indtaegt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Born.Born_ID,
    Bornegruppe.Bornegruppe_ID,
    Bornegruppe.Gruppenavn,
    Born.Navn,
    Born.Adresse,
    Born.Telefon,
    Born.AntalSolgteLodseddeler,
    Bornegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtaegt
JOIN Salg        ON Indtaegt.Salg_ID = Salg.Salg_ID
JOIN Baern        ON Salg.Born_ID    = Born.Born_ID
JOIN Bornegruppe ON Bornegruppe.Bornegruppe_ID = Born.Bornegruppe_ID
ORDER BY Indtaegt.Indtaegt_ID ASC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtaegt
                {
                    Indtaegt_ID = r.GetInt32(r.GetOrdinal("Indtaegt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Born = new Born
                    {
                        Born_ID = r.GetInt32(r.GetOrdinal("Born_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Bornegruppe = new Bornegruppe
                    {
                        Bornegruppe_ID = r.GetInt32(r.GetOrdinal("Bornegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public List<Indtaegt> GetAntalSolgteLodseddelerDESC()
        {
            var list = new List<Indtaegt>();
            const string sql = @"
SELECT
    Indtaegt.Indtaegt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Born.Born_ID,
    Bornegruppe.Bornegruppe_ID,
    Bornegruppe.Gruppenavn,
    Born.Navn,
    Born.Adresse,
    Born.Telefon,
    Born.AntalSolgteLodseddeler,
    Bornegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtaegt
JOIN Salg        ON Indtaegt.Salg_ID = Salg.Salg_ID
JOIN Born        ON Salg.Born_ID    = Born.Born_ID
JOIN Bornegruppe ON Bornegruppe.Bornegruppe_ID = Born.Bornegruppe_ID
ORDER BY Born.AntalSolgteLodseddeler DESC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtaegt
                {
                    Indtaegt_ID = r.GetInt32(r.GetOrdinal("Indtaegt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Born = new Born
                    {
                        Born_ID = r.GetInt32(r.GetOrdinal("Born_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Bornegruppe = new Bornegruppe
                    {
                        Bornegruppe_ID = r.GetInt32(r.GetOrdinal("Bornegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public List<Indtaegt> GetAntalSolgteLodseddelerASC()
        {
            var list = new List<Indtaegt>();
            const string sql = @"
SELECT
    Indtaegt.Indtaegt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Born.Born_ID,
    Bornegruppe.Bornegruppe_ID,
    Bornegruppe.Gruppenavn,
    Born.Navn,
    Born.Adresse,
    Born.Telefon,
    Born.AntalSolgteLodseddeler,
    Bornegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtaegt
JOIN Salg        ON Indtaegt.Salg_ID = Salg.Salg_ID
JOIN Born        ON Salg.Born_ID    = Born.Born_ID
JOIN Bornegruppe ON Bornegruppe.Bornegruppe_ID = Born.Bornegruppe_ID
ORDER BY Born.AntalSolgteLodseddeler ASC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtaegt
                {
                    Indtaegt_ID = r.GetInt32(r.GetOrdinal("Indtaegt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Born = new Born
                    {
                        Born_ID = r.GetInt32(r.GetOrdinal("Born_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Bornegruppe = new Bornegruppe
                    {
                        Bornegruppe_ID = r.GetInt32(r.GetOrdinal("Bornegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public List<Indtaegt> GetAntalSolgteLodseddelerForGruppenASC()
        {
            var list = new List<Indtaegt>();
            const string sql = @"
SELECT
    Indtaegt.Indtaegt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Born.Born_ID,
    Bornegruppe.Bornegruppe_ID,
    Bornegruppe.Gruppenavn,
    Born.Navn,
    Born.Adresse,
    Born.Telefon,
    Born.AntalSolgteLodseddeler,
    Bornegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtaegt
JOIN Salg        ON Indtaegt.Salg_ID = Salg.Salg_ID
JOIN Born        ON Salg.Born_ID    = Born.Born_ID
JOIN Bornegruppe ON Bornegruppe.Bornegruppe_ID = Born.Bornegruppe_ID
ORDER BY Bornegruppe.AntalSolgteLodseddelerPrGruppe ASC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtaegt
                {
                    Indtaegt_ID = r.GetInt32(r.GetOrdinal("Indtaegt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Born = new Born
                    {
                        Born_ID = r.GetInt32(r.GetOrdinal("Born_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Bornegruppe = new Bornegruppe
                    {
                        Bornegruppe_ID = r.GetInt32(r.GetOrdinal("Bornegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public List<Indtaegt> GetAntalSolgteLodseddelerForGruppenDESC()
        {
            var list = new List<Indtaegt>();
            const string sql = @"
SELECT
    Indtaegt.Indtaegt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Born.Born_ID,
    Bornegruppe.Bornegruppe_ID,
    Bornegruppe.Gruppenavn,
    Born.Navn,
    Born.Adresse,
    Born.Telefon,
    Born.AntalSolgteLodseddeler,
    Bornegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtaegt
JOIN Salg        ON Indtaegt.Salg_ID = Salg.Salg_ID
JOIN Born        ON Salg.Born_ID    = Born.Born_ID
JOIN Bornegruppe ON Bornegruppe.Bornegruppe_ID = Born.Bornegruppe_ID
ORDER BY Bornegruppe.AntalSolgteLodseddelerPrGruppe DESC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtaegt
                {
                    Indtaegt_ID = r.GetInt32(r.GetOrdinal("Indtaegt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Born = new Born
                    {
                        Born_ID = r.GetInt32(r.GetOrdinal("Born_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Bornegruppe = new Bornegruppe
                    {
                        Bornegruppe_ID = r.GetInt32(r.GetOrdinal("Bornegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }
    }
}
