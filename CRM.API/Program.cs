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
    builder.Configuration.GetConnectionString("KlantenConnection")));
builder.Services.AddScoped<IKlantenRepository, SQLKlantenRepository>();
builder.Services.AddScoped<IFactuurRepository, SQLFactuurRepository>();
builder.Services.AddScoped<ILandRepository, SQLLandRepository>();
builder.Services.AddScoped<IAccountRepository, SQLAccountRepository>();


builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
          builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
