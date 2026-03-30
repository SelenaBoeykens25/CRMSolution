using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using CRM.API.Repo;
using CRM.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(
                "http://crmportfolio.runasp.net", 
                "https://crmportfolio.runasp.net",
                "http://crmportfolioangular.runasp.net", 
                "https://crmportfolioangular.runasp.net")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddDbContext<KlantenDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("KlantenConnection"),
    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null)));
builder.Services.AddScoped<IKlantenRepository, SQLKlantenRepository>();
builder.Services.AddScoped<IFactuurRepository, SQLFactuurRepository>();
builder.Services.AddScoped<ILandRepository, SQLLandRepository>();
builder.Services.AddScoped<IAccountRepository, SQLAccountRepository>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure CORS before other middleware
if (app.Environment.IsDevelopment())
{
    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}
else
{
    app.UseCors("Frontend");
}

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference();

if (app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

// Test database connection endpoint (for development only)
if (app.Environment.IsDevelopment())
{
    app.MapGet("/test-db-connection", async (IConfiguration config) =>
    {
        var connectionString = config.GetConnectionString("KlantenConnection");
        var result = await CRM.API.TestConnection.TestDatabaseConnectionAsync(connectionString!);
        return result ? Results.Ok("Database connection successful!") : Results.Problem("Database connection failed!");
    });
}

app.Run();
