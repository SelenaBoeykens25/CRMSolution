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
builder.Services.AddCors();
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
app.UseCors(builder =>
{
    if (app.Environment.IsDevelopment())
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    }
    else
    {
        builder.WithOrigins("http://crmportfolio.runasp.net", "https://crmportfolio.runasp.net")
               .AllowAnyHeader()
               .AllowAnyMethod();
    }
});

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Don't redirect HTTP to HTTPS in production when using HTTP-only hosting
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
