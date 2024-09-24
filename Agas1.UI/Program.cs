using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Agas1.UI.Data;
using Agas1.Logic;
using Agas1.Logic.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddDbContext<DistilleryContext>(options =>
    options.UseSqlite("Data Source=distillery.db"));  // Ensure DbContext is configured with SQLite
builder.Services.AddScoped<DistilleryService>();
builder.Services.AddScoped<LiquidTypeService>();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();


// Ensure the database is created at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DistilleryContext>();
    dbContext.Database.EnsureCreated();  // Create the database if it doesn't exist
}

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
