using LodSalgsSystemFDF.Services.ADOServices.ADOIndt�gtService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

using LodSalgsSystemFDF.Services.ADOServices.ADOB�rnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using LodSalgsSystemFDF.Services.ADOServices.ADOSalgService;
using LodSalgsSystemFDF.Services.ADOServices.ADOB�rnService;
using LodSalgsSystemFDF.Services.ADOServices.ADOLederService;
using LodSalgsSystemFDF.Services.ADOServices.BrugerService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
//ADONET
builder.Services.AddTransient<AdonetIndt�gtService>();
builder.Services.AddTransient<IIndt�gtService, Indt�gtService>();
builder.Services.AddTransient<AdonetB�rnegruppeService>();
builder.Services.AddTransient<IB�rnegruppeService, B�rnegruppeService>();
builder.Services.AddTransient<AdonetSalgService>();
builder.Services.AddTransient<ISalgService,SalgService>();
builder.Services.AddTransient<AdonetLederService>();
builder.Services.AddTransient<ILederService, LederService>();
builder.Services.AddTransient<AdonetB�rnService>(); 
builder.Services.AddTransient<IB�rnService, B�rnService>();
builder.Services.AddTransient<AdonetBrugerService>();
builder.Services.AddTransient<BrugerService, BrugerService>();
//GENERIC
builder.Services.AddTransient<GenericRepository<Salg>>();
builder.Services.AddTransient<IGenericRepository<Salg>, GenericRepository<Salg>>();
builder.Services.AddTransient<GenericRepository<B�rn>>();
builder.Services.AddTransient<IGenericRepository<B�rn>, GenericRepository<B�rn>>();
builder.Services.AddTransient<B�rnRepository>();
builder.Services.Configure<CookiePolicyOptions>(options => {
    // This lambda determines whether user consent for non-essential cookies is needed for a given request. options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.LoginPath = "/Login/LogInPage";

});
builder.Services.AddMvc().AddRazorPagesOptions(options => {
    options.Conventions.AuthorizeFolder("/B�rnegrupper");

}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
