using LodSalgsSystemFDF.Services.ADOServices.ADOIndtægtService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

using LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using LodSalgsSystemFDF.Services.ADOServices.ADOSalgService;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
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
builder.Services.AddTransient<AdonetIndtægtService>();
builder.Services.AddTransient<IIndtægtService, IndtægtService>();
builder.Services.AddTransient<AdonetBørnegruppeService>();
builder.Services.AddTransient<IBørnegruppeService, BørnegruppeService>();
builder.Services.AddTransient<AdonetSalgService>();
builder.Services.AddTransient<ISalgService,SalgService>();
builder.Services.AddTransient<AdonetLederService>();
builder.Services.AddTransient<ILederService, LederService>();
builder.Services.AddTransient<AdonetBørnService>(); 
builder.Services.AddTransient<IBørnService, BørnService>();
builder.Services.AddTransient<AdonetBrugerService>();
builder.Services.AddTransient<BrugerService, BrugerService>();
//GENERIC
builder.Services.AddTransient<GenericRepository<Salg>>();
builder.Services.AddTransient<IGenericRepository<Salg>, GenericRepository<Salg>>();
builder.Services.AddTransient<GenericRepository<Børn>>();
builder.Services.AddTransient<IGenericRepository<Børn>, GenericRepository<Børn>>();
builder.Services.AddTransient<BørnRepository>();
builder.Services.Configure<CookiePolicyOptions>(options => {
    // This lambda determines whether user consent for non-essential cookies is needed for a given request. options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.LoginPath = "/Login/LogInPage";

});
builder.Services.AddMvc().AddRazorPagesOptions(options => {
    options.Conventions.AuthorizeFolder("/Børnegrupper");

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
