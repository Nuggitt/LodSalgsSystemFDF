using LodSalgsSystemFDF.Services.ADOServices.ADOIndt�gtService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

using LodSalgsSystemFDF.Services.ADOServices.ADOB�rnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using LodSalgsSystemFDF.Services.ADOServices.ADOSalgService;
using LodSalgsSystemFDF.Services.ADOServices.ADOB�rnService;
using LodSalgsSystemFDF.Services.ADOServices.ADOLederService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
