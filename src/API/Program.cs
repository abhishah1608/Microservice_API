using api.Application.Services;
using api.Domain.Interfaces;
using api.Infrastructure.Repository;
using System.Data.SqlClient;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Template.API.Validations;

var builder = WebApplication.CreateBuilder(args);

string configPath = Directory.GetCurrentDirectory() + "\\" + "Config";


string envfile = builder.Environment.EnvironmentName == "Development" ? "dev" : builder.Environment.EnvironmentName == "Staging" ? "stag" : "prod"; 

builder.Configuration
    .SetBasePath(configPath) // Optional, if needed for path resolution
    .AddJsonFile($"appsettings-{envfile}.json", optional: true, reloadOnChange: true) // Environment-specific settings
    .AddEnvironmentVariables(); // Optional: if you're using environment variables



builder.Services.AddScoped<SqlConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register Swagger generator
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());


builder.Services.AddScoped<UserService>();

// Add services.
builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.MapControllers();

app.Run();
