using GameLogger.Models;
using GameLogger.Controllers;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseStaticFiles();   // tillater CSS, JS, bilder osv.
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Games}/{action=Index}/{id?}"); //standard rute
app.Run();