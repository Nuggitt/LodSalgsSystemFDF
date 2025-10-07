using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.ADOB�rnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOB�rnService;
using LodSalgsSystemFDF.Services.ADOServices.ADOIndt�gtService;
using LodSalgsSystemFDF.Services.ADOServices.ADOLederService;
using LodSalgsSystemFDF.Services.ADOServices.ADOSalgService;
using LodSalgsSystemFDF.Services.ADOServices.BrugerService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// --- K�r SQLite init (opretter DB, tabeller og dine data hvis tom) ---
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
await EnsureSqliteInitializedAsync(cs);

// S�rg for at vores logs kommer i Output (Debug)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger("DBDEBUG");

// Print den fulde sti til .db-filen
var csb = new SqliteConnectionStringBuilder(cs ?? "");
logger.LogInformation("SQLite file: {path}", System.IO.Path.GetFullPath(csb.DataSource));

// Print alle brugere (s� vi ser hvad login kigger i)
using (var c = new SqliteConnection(cs))
{
    c.Open();
    using var cmd = c.CreateCommand();
    cmd.CommandText = "SELECT BrugerNavn FROM Bruger ORDER BY BrugerNavn";
    using var r = cmd.ExecuteReader();

    logger.LogInformation("== Bruger-tabellen ==");
    while (r.Read()) logger.LogInformation("{navn}", r.GetString(0));
}


// ---------- Services ----------
builder.Services.AddRazorPages(options =>
{
    // G�r hele sitet offentligt som default
    options.Conventions.AllowAnonymousToFolder("/");
});

// Authentication + Authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(cookieOptions =>
    {
        cookieOptions.LoginPath = "/Login/LogInPage"; // kun relevant hvis man rammer en BESKYTTET side
    });
builder.Services.AddAuthorization(); // <- n�dvendig n�r du kalder app.UseAuthorization()

// ADONET
builder.Services.AddTransient<AdonetIndt�gtService>();
builder.Services.AddTransient<IIndt�gtService, Indt�gtService>();
builder.Services.AddTransient<AdonetB�rnegruppeService>();
builder.Services.AddTransient<IB�rnegruppeService, B�rnegruppeService>();
builder.Services.AddTransient<AdonetSalgService>();
builder.Services.AddTransient<ISalgService, SalgService>();
builder.Services.AddTransient<AdonetLederService>();
builder.Services.AddTransient<ILederService, LederService>();
builder.Services.AddTransient<AdonetB�rnService>();
builder.Services.AddTransient<IB�rnService, B�rnService>();
builder.Services.AddScoped<AdonetBrugerService>();
builder.Services.AddScoped<BrugerService>();

// GENERIC
builder.Services.AddTransient<GenericRepository<Salg>>();
builder.Services.AddTransient<IGenericRepository<Salg>, GenericRepository<Salg>>();
builder.Services.AddTransient<GenericRepository<B�rn>>();
builder.Services.AddTransient<IGenericRepository<B�rn>, GenericRepository<B�rn>>();
builder.Services.AddTransient<B�rnRepository>();

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
app.UseAuthentication();   // f�r UseAuthorization
app.UseAuthorization();

app.MapRazorPages();

app.Run();


// --- SQLite INIT: skaber DB + dine tabeller + seed v�rdier, hvis tom ---
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
DROP TABLE IF EXISTS B�rnegruppe;
CREATE TABLE IF NOT EXISTS B�rnegruppe (
  B�rnegruppe_ID   INTEGER PRIMARY KEY AUTOINCREMENT,
  Gruppenavn       VARCHAR(50)  NOT NULL,
  Lokale           VARCHAR(50)  NOT NULL,
  Antalb�rn        INT          NOT NULL,
  Leder_ID         INT          NOT NULL,
  AntalLodSeddelerPrGruppe       INT NOT NULL DEFAULT 0,
  AntalSolgteLodSeddelerPrGruppe INT NOT NULL DEFAULT 0
);
DROP TABLE IF EXISTS B�rn;
CREATE TABLE IF NOT EXISTS B�rn (
  B�rn_ID                INTEGER PRIMARY KEY AUTOINCREMENT,
  Navn                   VARCHAR(50)  NOT NULL,
  Adresse                VARCHAR(50)  NOT NULL,
  Telefon                VARCHAR(50)  NOT NULL,
  B�rnegruppe_ID         INTEGER      NOT NULL,
  GivetLodsedler         INTEGER      NOT NULL DEFAULT 0,
  AntalSolgteLodseddeler INTEGER      NOT NULL DEFAULT 0
);
CREATE TABLE IF NOT EXISTS Indt�gt (
    Indt�gt_ID INT NOT NULL,
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
  B�rnegruppe_ID    INT          NOT NULL
);

DROP TABLE IF EXISTS Salg;
DROP TABLE IF EXISTS Salg;
CREATE TABLE IF NOT EXISTS Salg (
  Salg_ID                        INTEGER PRIMARY KEY AUTOINCREMENT,
  B�rn_ID                        INT        NOT NULL,
  B�rnegruppe_ID                 INT        NOT NULL,
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

    if (await CountAsync("B�rnegruppe") == 0)
    {
        var sql = @"
INSERT INTO B�rnegruppe (B�rnegruppe_ID, Gruppenavn, Lokale, Antalb�rn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe) VALUES (1,'Puslinger','1E',6,1,0,0);
INSERT INTO B�rnegruppe (B�rnegruppe_ID, Gruppenavn, Lokale, Antalb�rn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe) VALUES (2,'R�vene','6F',10,2,0,0);
INSERT INTO B�rnegruppe (B�rnegruppe_ID, Gruppenavn, Lokale, Antalb�rn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe) VALUES (3,'Uglerne','4J',15,3,0,0);
INSERT INTO B�rnegruppe (B�rnegruppe_ID, Gruppenavn, Lokale, Antalb�rn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe) VALUES (4,'Ulvene','UE69',12,2,0,0);
INSERT INTO B�rnegruppe (B�rnegruppe_ID, Gruppenavn, Lokale, Antalb�rn, Leder_ID, AntalLodSeddelerPrGruppe, AntalSolgteLodSeddelerPrGruppe) VALUES (69,'testergruppe','testerhavn',0,1,85,15);
";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    if (await CountAsync("B�rn") == 0)
    {
        var sql = @"
INSERT INTO B�rn (B�rn_ID, Navn, Adresse, Telefon, B�rnegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (1,'Eva','Roskildevej 123, 2610 R�dovre','26917212',1,0,0);
INSERT INTO B�rn (B�rn_ID, Navn, Adresse, Telefon, B�rnegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (2,'Adam','Munkeleddet 321, 2720 Vanl�se','27917291',2,0,0);
INSERT INTO B�rn (B�rn_ID, Navn, Adresse, Telefon, B�rnegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (3,'Peter','Stenbjerg 49, 2600 Albertslund','46271917',3,0,0);
INSERT INTO B�rn (B�rn_ID, Navn, Adresse, Telefon, B�rnegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (4,'Tom','T�rnvej 21 2500 Valby','66559876',4,0,0);
INSERT INTO B�rn (B�rn_ID, Navn, Adresse, Telefon, B�rnegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (5,'Benedicte','Frederiksberg All� 21 2000 Frederiksberg','84332561',4,0,0);
INSERT INTO B�rn (B�rn_ID, Navn, Adresse, Telefon, B�rnegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (69,'B�geTess','testerhavn','69696969',69,5,15);
INSERT INTO B�rn (B�rn_ID, Navn, Adresse, Telefon, B�rnegruppe_ID, GivetLodsedler, AntalSolgteLodseddeler) VALUES (70,'B�getester','testerhavn','70707070',69,0,0);
";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    if (await CountAsync("Leder") == 0)
    {
        var sql = @"
INSERT INTO Leder (Leder_ID, Navn, Adresse, Telefon, Email, ErLotteriBestyrer, B�rnegruppe_ID) VALUES (1,'Kristof','Gadevej 4 2720 Vanl�se',' 22338678','mrkristof@gmail.com',0,1);
INSERT INTO Leder (Leder_ID, Navn, Adresse, Telefon, Email, ErLotteriBestyrer, B�rnegruppe_ID) VALUES (2,'Emma','Allevej 9 Hvidovre','23456789','emmadj@gmail.com',0,2);
INSERT INTO Leder (Leder_ID, Navn, Adresse, Telefon, Email, ErLotteriBestyrer, B�rnegruppe_ID) VALUES (3,'Jack','Vejgade 5 Valby','87654321','captainjack@gmail.com',0,3);
INSERT INTO Leder (Leder_ID, Navn, Adresse, Telefon, Email, ErLotteriBestyrer, B�rnegruppe_ID) VALUES (4,'Peter','Stenagervej 21 Glostrup','21232134','Ppeter@gmail.com',0,4);
";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    if (await CountAsync("Salg") == 0)
    {
        var sql = @"
INSERT INTO Salg (Salg_ID, B�rn_ID, B�rnegruppe_ID, Leder_ID, Dato, AntalLodseddelerRetur, AntalSolgteLodseddelerPrSalg, Pris)
VALUES (69, 69, 69, 1, '2023-12-18 15:39:00', 5, 15, 300);
";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    if (await CountAsync("Indt�gt") == 0)
    {
        var sql = @"INSERT INTO Indt�gt (Indt�gt_ID, Salg_ID) VALUES (69, 69);";
        using var cmd = conn.CreateCommand();
        cmd.Transaction = (SqliteTransaction)tx;
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    await tx.CommitAsync();
}
