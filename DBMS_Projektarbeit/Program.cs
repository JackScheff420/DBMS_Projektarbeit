using DBMS_Projektarbeit.Data;
using DBMS_Projektarbeit.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDatabase")));

builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddDbContext<LibraryContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDatabase")));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSyncfusionBlazor();


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

