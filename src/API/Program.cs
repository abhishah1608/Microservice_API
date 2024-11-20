using api.Application.Services;
using api.Domain.Interfaces;
using api.Infrastructure.Repository;
using System.Data.SqlClient;
using FluentValidation.AspNetCore;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using api.Application.interfaces;
using System.Text.Json.Serialization;
using api.Application.Validators;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);

        // Add Config folder.
        string configPath = Directory.GetCurrentDirectory() + "//" + "Config";

        // Configure Kestrel for HTTPS with SSL certificates
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            // Use HTTPS with the certificate and key provided
            serverOptions.ListenAnyIP(443, listenOptions =>
            {
                listenOptions.UseHttps("/app/Certs/mycert.pfx", "Krishna123@."); // Adjust paths as necessary
            });
        });

        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true; // Include API version in the response headers
            options.AssumeDefaultVersionWhenUnspecified = true; // Use a default version if not specified
            options.DefaultApiVersion = new ApiVersion(1, 0); // Set the default version to 1.0
        });

        builder.Services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV"; // Group by version (e.g., "v1")
            options.SubstituteApiVersionInUrl = true;
        });


        //string configPath = Directory.GetCurrentDirectory() + "\\" + "Config";

        string envfile = builder.Environment.EnvironmentName == "Development" ? "dev" : builder.Environment.EnvironmentName == "Staging" ? "stag" : "prod";

        // set path of appsettings-envfile.json.
        builder.Configuration
            .SetBasePath(configPath) // Optional, if needed for path resolution
            .AddJsonFile($"appsettings-{envfile}.json", optional: true, reloadOnChange: true) // Environment-specific settings
            .AddEnvironmentVariables(); // Optional: if you're using environment variables



        builder.Services.AddScoped(sp =>
            new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));


        // Register the data cache service as a singleton
        builder.Services.AddSingleton<IDataCacheService, DataCacheService>();

        // Register the startup task as a hosted service
        builder.Services.AddHostedService<StartupTask>();


        // Enable Cross Origin Resource Sharing.
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                builder =>
                {
                    // You can allow any specific origins, methods, and headers
                    //builder.WithOrigins("https://example.com", "http://localhost:4200") // Specify the allowed origins
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()   // Allow any headers (or you can restrict it)
                    .AllowAnyMethod();  // Allow any HTTP methods (GET, POST, etc.)
                });
        });


        // Set Custom Error validation from API.
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });


        // Add controller and Add Validation filter.
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        })
            // Here Remove default property return in json.
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        });

        // Add fluent API Validation on API request body.
        builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

        builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserLoginValidator>());

        // Add list of services.
        // list of all services.
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // Add HttpClient service
        builder.Services.AddHttpClient<IApiRepository, ApiRepository>();


        // Add swagger: Production environment does not add swagger, otherwise add swagger on DEV and stagging environment.
        if (envfile != "prod")
        {
            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                c.EnableAnnotations();

                // Configure JWT Bearer Token in Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter your JWT with the prefix 'Bearer '",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });
        }

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            // Register Swagger generator
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger/index.html"));
        }

        app.UseCors("AllowSpecificOrigins");

        app.MapControllers();

        app.Run();
    }

}
