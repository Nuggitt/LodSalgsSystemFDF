using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornService;
using LodSalgsSystemFDF.Services.ADOServices.ADOIndtaegtService;
using LodSalgsSystemFDF.Services.ADOServices.ADOLederService;
using LodSalgsSystemFDF.Services.ADOServices.ADOSalgService;
using LodSalgsSystemFDF.Services.ADOServices.BrugerService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// 1) Prøv env var først (så du kan overstyre lokalt)
var dbPath = Environment.GetEnvironmentVariable("DB_PATH");

// 2) Hvis ikke sat, brug /tmp (skrivbar men ikke persistent)
if (string.IsNullOrWhiteSpace(dbPath))
{
    Directory.CreateDirectory("/tmp/lodsalg"); // no-op hvis findes
    dbPath = Path.Combine("/tmp/lodsalg", "LodSalgsSystemDB.db");
}

// (Valgfrit) Seed fra App_Data hvis filen ikke findes endnu
var seedPath = Path.Combine(builder.Environment.ContentRootPath, "App_Data", "LodSalgsSystemDB.db");
if (!File.Exists(dbPath) && File.Exists(seedPath))
{
    try
    {
        File.Copy(seedPath, dbPath, overwrite: false);
    }
    catch { /* best effort */ }
}

// Overstyr conn string til at pege på /tmp
builder.Configuration["ConnectionStrings:DefaultConnection"] = $"Data Source={dbPath}";

// Init DB/tabeller hvis tom
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
await EnsureSqliteInitializedAsync(cs);




// ---------- Services ----------
builder.Services.AddRazorPages(options =>
{
    // Gør hele sitet offentligt som default
    options.Conventions.AllowAnonymousToFolder("/");
});

// Authentication + Authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(cookieOptions =>
    {
        cookieOptions.LoginPath = "/Login/LogInPage"; // kun relevant hvis man rammer en BESKYTTET side
    });
builder.Services.AddAuthorization(); // <- nødvendig når du kalder app.UseAuthorization()

// ADONET
builder.Services.AddTransient<AdonetIndtaegtService>();
builder.Services.AddTransient<IIndtaegtService, IndtaegtService>();
builder.Services.AddTransient<AdonetBornegruppeService>();
builder.Services.AddTransient<IBornegruppeService, BornegruppeService>();
builder.Services.AddTransient<AdonetSalgService>();
builder.Services.AddTransient<ISalgService, SalgService>();
builder.Services.AddTransient<AdonetLederService>();
builder.Services.AddTransient<ILederService, LederService>();
builder.Services.AddTransient<AdonetBornService>();
builder.Services.AddTransient<IBornService, BornService>();
builder.Services.AddScoped<AdonetBrugerService>();
builder.Services.AddScoped<BrugerService>();

// GENERIC
builder.Services.AddTransient<GenericRepository<Salg>>();
builder.Services.AddTransient<IGenericRepository<Salg>, GenericRepository<Salg>>();
builder.Services.AddTransient<GenericRepository<Born>>();
builder.Services.AddTransient<IGenericRepository<Born>, GenericRepository<Born>>();
builder.Services.AddTransient<BornRepository>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// ---------- App pipeline ----------
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();   // før UseAuthorization
app.UseAuthorization();

app.MapRazorPages();

app.Run();


static async Task EnsureSqliteInitializedAsync(string cs)
{
    using var conn = new SqliteConnection(cs);
    await conn.OpenAsync();
    using var tx = await conn.BeginTransactionAsync();

    // Drop og opret alle tabeller på ny
    var createSql = @"
DROP TABLE IF EXISTS Indtaegt;
DROP TABLE IF EXISTS Salg;
DROP TABLE IF EXISTS Born;
DROP TABLE IF EXISTS Bornegruppe;
DROP TABLE IF EXISTS Leder;
DROP TABLE IF EXISTS Bruger;

CREATE TABLE Bruger (
    BrugerNavn NVARCHAR(50) NOT NULL,
    Password   NVARCHAR(255) NOT NULL
);
CREATE UNIQUE INDEX IF NOT EXISTS IX_Bruger_BrugerNavn_CI ON Bruger(lower(BrugerNavn));

CREATE TABLE Bornegruppe (
  Bornegruppe_ID   INTEGER PRIMARY KEY AUTOINCREMENT,
  Gruppenavn       VARCHAR(50)  NOT NULL,
  Lokale           VARCHAR(50)  NOT NULL,
  Antalborn        INT          NOT NULL,
  Leder_ID         INT          NOT NULL,
  AntalLodSeddelerPrGruppe       INT NOT NULL DEFAULT 0,
  AntalSolgteLodSeddelerPrGruppe INT NOT NULL DEFAULT 0
);

CREATE TABLE Born (
  Born_ID                INTEGER PRIMARY KEY AUTOINCREMENT,
  Navn                   VARCHAR(50)  NOT NULL,
  Adresse                VARCHAR(50)  NOT NULL,
  Telefon                VARCHAR(50)  NOT NULL,
  Bornegruppe_ID         INTEGER      NOT NULL,
  GivetLodsedler         INTEGER      NOT NULL DEFAULT 0,
  AntalSolgteLodseddeler INTEGER      NOT NULL DEFAULT 0
);

CREATE TABLE Leder (
  Leder_ID          INTEGER PRIMARY KEY AUTOINCREMENT,
  Navn              VARCHAR(50)  NOT NULL,
  Adresse           VARCHAR(50)  NOT NULL,
  Telefon           VARCHAR(50)  NOT NULL,
  Email             VARCHAR(50)  NOT NULL,
  ErLotteriBestyrer BIT          NOT NULL,
  Bornegruppe_ID    INT          NOT NULL
);

CREATE TABLE Salg (
  Salg_ID                        INTEGER PRIMARY KEY AUTOINCREMENT,
  Born_ID                        INT        NOT NULL,
  Bornegruppe_ID                 INT        NOT NULL,
  Leder_ID                       INT        NOT NULL,
  Dato                           DATETIME   NOT NULL,
  AntalLodseddelerRetur          INT        NOT NULL DEFAULT 0,
  AntalSolgteLodseddelerPrSalg   INT        NOT NULL DEFAULT 0,
  Pris                           FLOAT      NOT NULL
);

CREATE TABLE Indtaegt (
    Indtaegt_ID INT NOT NULL,
    Salg_ID     INT NOT NULL
);
";
    using (var cmd = conn.CreateCommand())
    {
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = createSql;
        await cmd.ExecuteNonQueryAsync();
    }

    // ---- Ny demo-data ----
    var sql = @"
-- Brugere (beholdes ASCII-safe)
INSERT INTO Bruger (BrugerNavn, Password) VALUES
('admin','AQAAAAIAAYagAAAAECUGL661u75+y5Iy15JdWohs+3fq1l1iMjooyPRLuyL7hCgpz01A5VL4dSVzNS+08A=='),
('bestyrer','AQAAAAIAAYagAAAAEC2fUJ/IlwPPx8l4Q0QWtKscC/LdVtjnh3VPanTkRFfr/uc2mCiwyHct4hPX780s/w=='),
('leder','AQAAAAIAAYagAAAAELmU0+AedrS5avgSvwZjLF1uB761TvCpc68UuUC+uVDd8QuwdHlwvarwY98lMsI+3A=='),
('tester','AQAAAAIAAYagAAAAEEFpctrlUPRgq4qLER4UNFgym9cau7G3b8imN/KswGQZdE0mDMm48Ymdew3DJ3Losw==');

-- Bornegrupper
INSERT INTO Bornegruppe (Bornegruppe_ID, Gruppenavn, Lokale, Antalborn, Leder_ID)
VALUES
(1,'Ravne','1A',10,1),
(2,'Egern','1B',8,2),
(3,'Falke','2A',12,3),
(4,'Ulve','2B',11,4),
(5,'Loever','3A',9,5),
(6,'Pantere','3B',13,6),
(7,'Hajer','4A',7,7),
(8,'Tiger','4B',10,8),
(9,'Slanger','5A',12,9),
(10,'Gribbe','5B',6,10);

-- Ledere
INSERT INTO Leder (Leder_ID, Navn, Adresse, Telefon, Email, ErLotteriBestyrer, Bornegruppe_ID)
VALUES
(1,'Mads Jensen','Parkvej 10 2600 Glostrup','20101010','madsj@example.com',0,1),
(2,'Signe Petersen','Stationsvej 2 2620 Albertslund','22222222','signep@example.com',0,2),
(3,'Jonas Nielsen','Soevej 7 2630 Taastrup','23232323','jonasn@example.com',0,3),
(4,'Clara Madsen','Enghavevej 11 2450 Kbh SV','24242424','claram@example.com',0,4),
(5,'Oliver Larsen','Skovvej 6 2700 Broenshoej','25252525','oliverl@example.com',0,5),
(6,'Sofia Kristensen','Hovedgaden 18 2625 Vallensbaek','26262626','sofiak@example.com',0,6),
(7,'Peter Jensen','Engtoften 5 2630 Taastrup','27272727','peterj@example.com',0,7),
(8,'Laura Mortensen','Byvej 3 2600 Glostrup','28282828','lauram@example.com',0,8),
(9,'Anders Thomsen','Vibevangen 9 2700 Broenshoej','29292929','anderst@example.com',0,9),
(10,'Emma Holm','Solsikkevej 15 2720 Vanloese','30303030','emmah@example.com',0,10);

-- Born
INSERT INTO Born (Born_ID, Navn, Adresse, Telefon, Bornegruppe_ID)
VALUES
(1,'Noah','Ringvej 1 2600 Glostrup','30101010',1),
(2,'Emil','Svanebaek 4 2720 Vanloese','31111111',1),
(3,'Ida','Skolevej 9 2500 Valby','32121212',2),
(4,'Oscar','Banegaardsvej 3 2605 Broendby','33131313',2),
(5,'Alma','Vestervej 5 2610 Roedovre','34141414',3),
(6,'Karl','Birkevej 8 2600 Glostrup','35151515',3),
(7,'Asta','Soeparken 2 2700 Broenshoej','36161616',4),
(8,'Malthe','Elmevej 12 2720 Vanloese','37171717',4),
(9,'Freja','Park Alle 1 2600 Glostrup','38181818',5),
(10,'Victor','Fjordvej 6 2630 Taastrup','39191919',5),
(11,'Maja','Kongevej 22 2620 Albertslund','40101010',6),
(12,'Laura','Nordrevej 7 2450 Kbh SV','41111111',6),
(13,'William','Aabyevej 3 2600 Glostrup','42121212',7),
(14,'Elias','Moellevej 5 2610 Roedovre','43131313',7),
(15,'Emma','Nordre Fasanvej 55 2000 FRB','44141414',8),
(16,'Magnus','Rugbjergvej 1 2720 Vanloese','45151515',8),
(17,'Ane','Solsikkevej 2 2700 Broenshoej','46161616',9),
(18,'Sofie','Lindevej 3 2625 Vallensbaek','47171717',9),
(19,'Liam','Engtoften 1 2630 Taastrup','48181818',10),
(20,'Nora','Havnevej 6 2450 Kbh SV','49191919',10);

-- Salg
INSERT INTO Salg (Salg_ID, Born_ID, Bornegruppe_ID, Leder_ID, Dato, AntalLodseddelerRetur, AntalSolgteLodseddelerPrSalg, Pris) VALUES
(1,1,1,1,'2024-01-10 14:00:00',0,5,100),
(2,2,1,1,'2024-01-15 16:30:00',1,3,60),
(3,3,2,2,'2024-02-01 12:00:00',0,8,160),
(4,4,2,2,'2024-02-03 11:10:00',0,2,40),
(5,5,3,3,'2024-02-20 10:20:00',0,10,200),
(6,6,3,3,'2024-02-22 17:45:00',0,4,80),
(7,9,5,5,'2024-03-05 13:00:00',0,7,140),
(8,10,5,5,'2024-03-06 15:20:00',2,5,100),
(9,11,6,6,'2024-03-10 09:35:00',0,6,120),
(10,12,6,6,'2024-03-12 18:25:00',0,3,60),
(11,13,7,7,'2024-03-18 14:55:00',0,9,180),
(12,14,7,7,'2024-03-19 16:05:00',1,2,40),
(13,15,8,8,'2024-03-25 11:15:00',0,5,100),
(14,16,8,8,'2024-03-26 12:45:00',0,6,120),
(15,17,9,9,'2024-04-02 10:10:00',0,8,160),
(16,18,9,9,'2024-04-04 17:00:00',0,3,60),
(17,19,10,10,'2024-04-08 13:30:00',0,4,80),
(18,20,10,10,'2024-04-09 15:00:00',0,7,140);

-- Indtaegt
INSERT INTO Indtaegt (Indtaegt_ID, Salg_ID)
SELECT Salg_ID, Salg_ID FROM Salg;

-- Aggreger totals
UPDATE Born
SET AntalSolgteLodseddeler = COALESCE((SELECT SUM(AntalSolgteLodseddelerPrSalg)
FROM Salg WHERE Salg.Born_ID = Born.Born_ID), 0),
GivetLodsedler = COALESCE((SELECT SUM(AntalSolgteLodseddelerPrSalg + AntalLodseddelerRetur)
FROM Salg WHERE Salg.Born_ID = Born.Born_ID), 0);

UPDATE Bornegruppe
SET AntalSolgteLodSeddelerPrGruppe = COALESCE((SELECT SUM(s.AntalSolgteLodseddelerPrSalg)
FROM Salg s WHERE s.Bornegruppe_ID = Bornegruppe.Bornegruppe_ID), 0);
";
    using (var cmd = conn.CreateCommand())
    {
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    await tx.CommitAsync();
}
