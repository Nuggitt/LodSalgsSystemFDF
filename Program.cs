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
// Vælg sti til SQLite-db. På Render bruger vi en disk mountet på /var/data.
var dbPath = Environment.GetEnvironmentVariable("DB_PATH");
if (string.IsNullOrWhiteSpace(dbPath))
{
    // Lokalt / container uden env var: brug App_Data i projektet
    var appData = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
    Directory.CreateDirectory(appData);
    dbPath = Path.Combine(appData, "LodSalgsSystemDB.db");
}

// Overstyr conn string, så alt bruger samme sti
builder.Configuration["ConnectionStrings:DefaultConnection"] = $"Data Source={dbPath}";


// --- Kør SQLite init (opretter DB, tabeller og dine data hvis tom) ---
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


// --- SQLite INIT: skaber DB + dine tabeller + seed værdier, hvis tom ---
static async Task EnsureSqliteInitializedAsync(string cs)
{
    using var conn = new SqliteConnection(cs);
    await conn.OpenAsync();
    using var tx = await conn.BeginTransactionAsync();

    var createSql = @"
CREATE TABLE IF NOT EXISTS Bruger (
    BrugerNavn NVARCHAR(50) NOT NULL,
    Password   NVARCHAR(255) NOT NULL
);
CREATE UNIQUE INDEX IF NOT EXISTS IX_Bruger_BrugerNavn_CI
ON Bruger(lower(BrugerNavn)
);
DROP TABLE IF EXISTS Bornegruppe;
CREATE TABLE IF NOT EXISTS Bornegruppe (
  Bornegruppe_ID   INTEGER PRIMARY KEY AUTOINCREMENT,
  Gruppenavn       VARCHAR(50)  NOT NULL,
  Lokale           VARCHAR(50)  NOT NULL,
  Antalborn        INT          NOT NULL,
  Leder_ID         INT          NOT NULL,
  AntalLodSeddelerPrGruppe       INT NOT NULL DEFAULT 0,
  AntalSolgteLodSeddelerPrGruppe INT NOT NULL DEFAULT 0
);
DROP TABLE IF EXISTS Born;
CREATE TABLE IF NOT EXISTS Born (
  Born_ID                INTEGER PRIMARY KEY AUTOINCREMENT,
  Navn                   VARCHAR(50)  NOT NULL,
  Adresse                VARCHAR(50)  NOT NULL,
  Telefon                VARCHAR(50)  NOT NULL,
  Bornegruppe_ID         INTEGER      NOT NULL,
  GivetLodsedler         INTEGER      NOT NULL DEFAULT 0,
  AntalSolgteLodseddeler INTEGER      NOT NULL DEFAULT 0
);
CREATE TABLE IF NOT EXISTS Indtaegt (
    Indtaegt_ID INT NOT NULL,
    Salg_ID    INT NOT NULL
);
DROP TABLE IF EXISTS Leder;
CREATE TABLE IF NOT EXISTS Leder (
  Leder_ID          INTEGER PRIMARY KEY AUTOINCREMENT,
  Navn              VARCHAR(50)  NOT NULL,
  Adresse           VARCHAR(50)  NOT NULL,
  Telefon           VARCHAR(50)  NOT NULL,
  Email             VARCHAR(50)  NOT NULL,
  ErLotteriBestyrer BIT          NOT NULL,
  Bornegruppe_ID    INT          NOT NULL
);

DROP TABLE IF EXISTS Salg;
DROP TABLE IF EXISTS Salg;
CREATE TABLE IF NOT EXISTS Salg (
  Salg_ID                        INTEGER PRIMARY KEY AUTOINCREMENT,
  Born_ID                        INT        NOT NULL,
  Bornegruppe_ID                 INT        NOT NULL,
  Leder_ID                       INT        NOT NULL,
  Dato                           DATETIME   NOT NULL,
  AntalLodseddelerRetur          INT        NOT NULL DEFAULT 0,
  AntalSolgteLodseddelerPrSalg   INT        NOT NULL DEFAULT 0,
  Pris                           FLOAT      NOT NULL
);
";
    using (var cmd = conn.CreateCommand())
    {
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = createSql;
        await cmd.ExecuteNonQueryAsync();
    }

    async Task<int> CountAsync(string table)
    {
        using var c = conn.CreateCommand();
        c.Transaction = (SqliteTransaction)tx;
        c.CommandText = $"SELECT COUNT(*) FROM {table};";
        var o = await c.ExecuteScalarAsync();
        return Convert.ToInt32(o);
    }

    if (await CountAsync("Bruger") == 0)
    {
        var sql = @"
INSERT INTO Bruger (BrugerNavn, Password) VALUES ('admin','AQAAAAIAAYagAAAAECUGL661u75+y5Iy15JdWohs+3fq1l1iMjooyPRLuyL7hCgpz01A5VL4dSVzNS+08A==');
INSERT INTO Bruger (BrugerNavn, Password) VALUES ('bestyrer','AQAAAAIAAYagAAAAEC2fUJ/IlwPPx8l4Q0QWtKscC/LdVtjnh3VPanTkRFfr/uc2mCiwyHct4hPX780s/w==');
INSERT INTO Bruger (BrugerNavn, Password) VALUES ('gey','AQAAAAIAAYagAAAAEGiwJBsSR80FV+NIQ66CPPIUffcY2LalCspAwWYp6qhlYypSj0/qkuiZM+NvdhdsLw==');
INSERT INTO Bruger (BrugerNavn, Password) VALUES ('leder','AQAAAAIAAYagAAAAELmU0+AedrS5avgSvwZjLF1uB761TvCpc68UuUC+uVDd8QuwdHlwvarwY98lMsI+3A==');
INSERT INTO Bruger (BrugerNavn, Password) VALUES ('lotteribestyrer','AQAAAAIAAYagAAAAEAtD+PmvlA5ppBgNRWcw7HOBYK0JqV1mknKHic+/2cihdk2xZTWgeaikMyq0D42sDA==');
INSERT INTO Bruger (BrugerNavn, Password) VALUES ('testerbruger','AQAAAAIAAYagAAAAEEFpctrlUPRgq4qLER4UNFgym9cau7G3b8imN/KswGQZdE0mDMm48Ymdew3DJ3Losw==');
";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    if (await CountAsync("Bornegruppe") == 0)
    {
        var sql = @"
INSERT INTO Bornegruppe (Bornegruppe_ID, Gruppenavn, Lokale, Antalborn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe) VALUES (1,'Puslinger','1E',6,1,0,0);
INSERT INTO Bornegruppe (Bornegruppe_ID, Gruppenavn, Lokale, Antalborn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe) VALUES (2,'Rævene','6F',10,2,0,0);
INSERT INTO Bornegruppe (Bornegruppe_ID, Gruppenavn, Lokale, Antalborn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe) VALUES (3,'Uglerne','4J',15,3,0,0);
INSERT INTO Bornegruppe (Bornegruppe_ID, Gruppenavn, Lokale, Antalborn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe) VALUES (4,'Ulvene','UE69',12,2,0,0);
INSERT INTO Bornegruppe (Bornegruppe_ID, Gruppenavn, Lokale, Antalborn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe) VALUES (69,'testergruppe','testerhavn',0,1,85,15);
";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    if (await CountAsync("Born") == 0)
    {
        var sql = @"
INSERT INTO Born (Born_ID, Navn, Adresse, Telefon, Bornegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (1,'Eva','Roskildevej 123, 2610 Rødovre','26917212',1,0,0);
INSERT INTO Born (Born_ID, Navn, Adresse, Telefon, Bornegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (2,'Adam','Munkeleddet 321, 2720 Vanløse','27917291',2,0,0);
INSERT INTO Born (Born_ID, Navn, Adresse, Telefon, Bornegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (3,'Peter','Stenbjerg 49, 2600 Albertslund','46271917',3,0,0);
INSERT INTO Born (Born_ID, Navn, Adresse, Telefon, Bornegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (4,'Tom','Tårnvej 21 2500 Valby','66559876',4,0,0);
INSERT INTO Born (Born_ID, Navn, Adresse, Telefon, Bornegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (5,'Benedicte','Frederiksberg Allé 21 2000 Frederiksberg','84332561',4,0,0);
INSERT INTO Born (Born_ID, Navn, Adresse, Telefon, Bornegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (69,'BøgeTess','testerhavn','69696969',69,5,15);
INSERT INTO Born (Born_ID, Navn, Adresse, Telefon, Bornegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (70,'Bøgetester','testerhavn','70707070',69,0,0);
";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    if (await CountAsync("Leder") == 0)
    {
        var sql = @"
INSERT INTO Leder (Leder_ID, Navn, Adresse, Telefon, Email, ErLotteriBestyrer, Bornegruppe_ID) VALUES (1,'Kristof','Gadevej 4 2720 Vanløse',' 22338678','mrkristof@gmail.com',0,1);
INSERT INTO Leder (Leder_ID, Navn, Adresse, Telefon, Email, ErLotteriBestyrer, Bornegruppe_ID) VALUES (2,'Emma','Allevej 9 Hvidovre','23456789','emmadj@gmail.com',0,2);
INSERT INTO Leder (Leder_ID, Navn, Adresse, Telefon, Email, ErLotteriBestyrer, Bornegruppe_ID) VALUES (3,'Jack','Vejgade 5 Valby','87654321','captainjack@gmail.com',0,3);
INSERT INTO Leder (Leder_ID, Navn, Adresse, Telefon, Email, ErLotteriBestyrer, Bornegruppe_ID) VALUES (4,'Peter','Stenagervej 21 Glostrup','21232134','Ppeter@gmail.com',0,4);
";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    if (await CountAsync("Salg") == 0)
    {
        var sql = @"
INSERT INTO Salg (Salg_ID, Born_ID, Bornegruppe_ID, Leder_ID, Dato, AntalLodseddelerRetur, AntalSolgteLodseddelerPrSalg, Pris)
VALUES (69, 69, 69, 1, '2023-12-18 15:39:00', 5, 15, 300);
";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    if (await CountAsync("Indtaegt") == 0)
    {
        var sql = @"INSERT INTO Indtaegt (Indtaegt_ID, Salg_ID) VALUES (69, 69);";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    await tx.CommitAsync();
}
