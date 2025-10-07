using LodSalgsSystemFDF.Models;
using Microsoft.Data.Sqlite;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOIndtægtService
{
    public class AdonetIndtægtService
    {
        private IConfiguration configuration { get; }
        private readonly string connectionString;

        public AdonetIndtægtService() { }

        public AdonetIndtægtService(IConfiguration config)
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

        public List<Indtægt> GetAllIndtægter()
        {
            var list = new List<Indtægt>();
            // Alias’er harmoniserer kolonnenavne i resultatsættet
            const string sql = @"
SELECT
    Indtægt.Indtægt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Børn.Børn_ID,
    Børnegruppe.Børnegruppe_ID,
    Børnegruppe.Gruppenavn,
    Børn.Navn,
    Børn.Adresse,
    Børn.Telefon,
    Børn.AntalSolgteLodseddeler,
    Børnegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtægt
JOIN Salg        ON Indtægt.Salg_ID = Salg.Salg_ID
JOIN Børn        ON Salg.Børn_ID    = Børn.Børn_ID
JOIN Børnegruppe ON Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtægt
                {
                    Indtægt_ID = r.GetInt32(r.GetOrdinal("Indtægt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Børn = new Børn
                    {
                        Børn_ID = r.GetInt32(r.GetOrdinal("Børn_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Børnegruppe = new Børnegruppe
                    {
                        Børnegruppe_ID = r.GetInt32(r.GetOrdinal("Børnegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public Indtægt GetIndtægtById(int id)
        {
            var ind = new Indtægt();
            const string sql = "SELECT * FROM Indtægt WHERE Indtægt_ID = @Indtægt_ID";
            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Indtægt_ID", id);
            using var r = cmd.ExecuteReader();
            if (r.Read())
            {
                ind.Indtægt_ID = r.GetInt32(r.GetOrdinal("Indtægt_ID"));
                ind.Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID"));
            }
            return ind;
        }

        public Indtægt CreateIndtægt(Indtægt indtægt)
        {
            // NB: ingen schema i SQLite, og kolonner må ikke være kvalificeret med tabelnavn
            const string sql = "INSERT INTO Indtægt (Indtægt_ID, Salg_ID) VALUES (@Indtægt_ID, @Salg_ID);";
            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Indtægt_ID", indtægt.Indtægt_ID);
            cmd.Parameters.AddWithValue("@Salg_ID", indtægt.Salg?.Salg_ID ?? indtægt.Salg_ID);
            cmd.ExecuteNonQuery();
            return indtægt;
        }

        public Indtægt DeleteIndtægt(Indtægt indtægt)
        {
            const string sql = "DELETE FROM Indtægt WHERE Indtægt_ID = @Indtægt_ID";
            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Indtægt_ID", indtægt.Indtægt_ID);
            cmd.ExecuteNonQuery();
            return indtægt;
        }

        public Indtægt UpdateIndtægt(Indtægt indtægt)
        {
            const string sql = "UPDATE Indtægt SET Salg_ID = @Salg_ID WHERE Indtægt_ID = @Indtægt_ID";
            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Indtægt_ID", indtægt.Indtægt_ID);
            cmd.Parameters.AddWithValue("@Salg_ID", indtægt.Salg_ID);
            cmd.ExecuteNonQuery();
            return indtægt;
        }

        public List<Indtægt> GetIndtægtIDDESC()
        {
            var list = new List<Indtægt>();
            const string sql = @"
SELECT
    Indtægt.Indtægt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Børn.Børn_ID,
    Børnegruppe.Børnegruppe_ID,
    Børnegruppe.Gruppenavn,
    Børn.Navn,
    Børn.Adresse,
    Børn.Telefon,
    Børn.AntalSolgteLodseddeler,
    Børnegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtægt
JOIN Salg        ON Indtægt.Salg_ID = Salg.Salg_ID
JOIN Børn        ON Salg.Børn_ID    = Børn.Børn_ID
JOIN Børnegruppe ON Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID
ORDER BY Indtægt.Indtægt_ID DESC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtægt
                {
                    Indtægt_ID = r.GetInt32(r.GetOrdinal("Indtægt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Børn = new Børn
                    {
                        Børn_ID = r.GetInt32(r.GetOrdinal("Børn_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Børnegruppe = new Børnegruppe
                    {
                        Børnegruppe_ID = r.GetInt32(r.GetOrdinal("Børnegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public List<Indtægt> GetIndtægtIDASC()
        {
            var list = new List<Indtægt>();
            const string sql = @"
SELECT
    Indtægt.Indtægt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Børn.Børn_ID,
    Børnegruppe.Børnegruppe_ID,
    Børnegruppe.Gruppenavn,
    Børn.Navn,
    Børn.Adresse,
    Børn.Telefon,
    Børn.AntalSolgteLodseddeler,
    Børnegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtægt
JOIN Salg        ON Indtægt.Salg_ID = Salg.Salg_ID
JOIN Børn        ON Salg.Børn_ID    = Børn.Børn_ID
JOIN Børnegruppe ON Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID
ORDER BY Indtægt.Indtægt_ID ASC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtægt
                {
                    Indtægt_ID = r.GetInt32(r.GetOrdinal("Indtægt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Børn = new Børn
                    {
                        Børn_ID = r.GetInt32(r.GetOrdinal("Børn_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Børnegruppe = new Børnegruppe
                    {
                        Børnegruppe_ID = r.GetInt32(r.GetOrdinal("Børnegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public List<Indtægt> GetAntalSolgteLodseddelerDESC()
        {
            var list = new List<Indtægt>();
            const string sql = @"
SELECT
    Indtægt.Indtægt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Børn.Børn_ID,
    Børnegruppe.Børnegruppe_ID,
    Børnegruppe.Gruppenavn,
    Børn.Navn,
    Børn.Adresse,
    Børn.Telefon,
    Børn.AntalSolgteLodseddeler,
    Børnegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtægt
JOIN Salg        ON Indtægt.Salg_ID = Salg.Salg_ID
JOIN Børn        ON Salg.Børn_ID    = Børn.Børn_ID
JOIN Børnegruppe ON Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID
ORDER BY Børn.AntalSolgteLodseddeler DESC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtægt
                {
                    Indtægt_ID = r.GetInt32(r.GetOrdinal("Indtægt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Børn = new Børn
                    {
                        Børn_ID = r.GetInt32(r.GetOrdinal("Børn_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Børnegruppe = new Børnegruppe
                    {
                        Børnegruppe_ID = r.GetInt32(r.GetOrdinal("Børnegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public List<Indtægt> GetAntalSolgteLodseddelerASC()
        {
            var list = new List<Indtægt>();
            const string sql = @"
SELECT
    Indtægt.Indtægt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Børn.Børn_ID,
    Børnegruppe.Børnegruppe_ID,
    Børnegruppe.Gruppenavn,
    Børn.Navn,
    Børn.Adresse,
    Børn.Telefon,
    Børn.AntalSolgteLodseddeler,
    Børnegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtægt
JOIN Salg        ON Indtægt.Salg_ID = Salg.Salg_ID
JOIN Børn        ON Salg.Børn_ID    = Børn.Børn_ID
JOIN Børnegruppe ON Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID
ORDER BY Børn.AntalSolgteLodseddeler ASC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtægt
                {
                    Indtægt_ID = r.GetInt32(r.GetOrdinal("Indtægt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Børn = new Børn
                    {
                        Børn_ID = r.GetInt32(r.GetOrdinal("Børn_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Børnegruppe = new Børnegruppe
                    {
                        Børnegruppe_ID = r.GetInt32(r.GetOrdinal("Børnegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public List<Indtægt> GetAntalSolgteLodseddelerForGruppenASC()
        {
            var list = new List<Indtægt>();
            const string sql = @"
SELECT
    Indtægt.Indtægt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Børn.Børn_ID,
    Børnegruppe.Børnegruppe_ID,
    Børnegruppe.Gruppenavn,
    Børn.Navn,
    Børn.Adresse,
    Børn.Telefon,
    Børn.AntalSolgteLodseddeler,
    Børnegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtægt
JOIN Salg        ON Indtægt.Salg_ID = Salg.Salg_ID
JOIN Børn        ON Salg.Børn_ID    = Børn.Børn_ID
JOIN Børnegruppe ON Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID
ORDER BY Børnegruppe.AntalSolgteLodseddelerPrGruppe ASC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtægt
                {
                    Indtægt_ID = r.GetInt32(r.GetOrdinal("Indtægt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Børn = new Børn
                    {
                        Børn_ID = r.GetInt32(r.GetOrdinal("Børn_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Børnegruppe = new Børnegruppe
                    {
                        Børnegruppe_ID = r.GetInt32(r.GetOrdinal("Børnegruppe_ID")),
                        Gruppenavn = r.GetString(r.GetOrdinal("Gruppenavn")),
                        AntalSolgteLodseddelerPrGruppe = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddelerPrGruppe"))
                    }
                };
                list.Add(it);
            }
            return list;
        }

        public List<Indtægt> GetAntalSolgteLodseddelerForGruppenDESC()
        {
            var list = new List<Indtægt>();
            const string sql = @"
SELECT
    Indtægt.Indtægt_ID,
    Salg.Salg_ID,
    Salg.Dato,
    Børn.Børn_ID,
    Børnegruppe.Børnegruppe_ID,
    Børnegruppe.Gruppenavn,
    Børn.Navn,
    Børn.Adresse,
    Børn.Telefon,
    Børn.AntalSolgteLodseddeler,
    Børnegruppe.AntalSolgteLodSeddelerPrGruppe AS AntalSolgteLodseddelerPrGruppe
FROM Indtægt
JOIN Salg        ON Indtægt.Salg_ID = Salg.Salg_ID
JOIN Børn        ON Salg.Børn_ID    = Børn.Børn_ID
JOIN Børnegruppe ON Børnegruppe.Børnegruppe_ID = Børn.Børnegruppe_ID
ORDER BY Børnegruppe.AntalSolgteLodseddelerPrGruppe DESC;";

            using var con = new SqliteConnection(connectionString);
            con.Open();
            using var cmd = new SqliteCommand(sql, con);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var it = new Indtægt
                {
                    Indtægt_ID = r.GetInt32(r.GetOrdinal("Indtægt_ID")),
                    Salg_ID = r.GetInt32(r.GetOrdinal("Salg_ID")),
                    Salg = new Salg { Dato = ReadNullableDate(r, "Dato") ?? default },
                    Børn = new Børn
                    {
                        Børn_ID = r.GetInt32(r.GetOrdinal("Børn_ID")),
                        Navn = r.GetString(r.GetOrdinal("Navn")),
                        Adresse = r.GetString(r.GetOrdinal("Adresse")),
                        Telefon = r.GetString(r.GetOrdinal("Telefon")),
                        AntalSolgteLodseddeler = r.GetInt32(r.GetOrdinal("AntalSolgteLodseddeler"))
                    },
                    Børnegruppe = new Børnegruppe
                    {
                        Børnegruppe_ID = r.GetInt32(r.GetOrdinal("Børnegruppe_ID")),
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

