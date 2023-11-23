using LodSalgsSystemFDF.Services.ADOServices.ADOIndt�gtService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

using LodSalgsSystemFDF.Services.ADOServices.ADOB�rnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using LodSalgsSystemFDF.Services.ADOServices.ADOSalgService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddTransient<AdonetIndt�gtService>();
builder.Services.AddTransient<IIndt�gtService, Indt�gtService>();
builder.Services.AddTransient<AdonetB�rnegruppeService>();
builder.Services.AddTransient<IB�rnegruppeService, B�rnegruppeService>();
builder.Services.AddTransient<AdonetSalgService>();
builder.Services.AddTransient<ISalgService,SalgService>();

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
