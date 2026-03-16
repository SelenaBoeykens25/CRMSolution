using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using CRM.API.Repo;
using CRM.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddDbContext<KlantenDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("KlantenConnection")));
builder.Services.AddScoped<IKlantenRepository, SQLKlantenRepository>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseCors(builder =>
          builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
