using System.Data;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TallerMecanico.Api.Mapping;
using TallerMecanico.Api.Middleware;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Infrastructure.Data;
using TallerMecanico.Infrastructure.Repositories;
using TallerMecanico.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Registrar WorkshopContext con Pomelo (MySQL)
builder.Services.AddDbContext<WorkshopContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Default"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TallerMecanico API",
        Version = "v1",
        Description = "API para gestión de clientes, vehículos y servicios en TallerMecanico"
    });
});


builder.Services.AddApiVersioning(options =>
{
    // Reporta las versiones soportadas y obsoletas en los encabezados de respuesta
    options.ReportApiVersions = true;

    // Versión por defecto si no se especifica
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Soporta versionado mediante URL, Header o QueryString
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),           // Ejemplo: /api/v1/...
        new HeaderApiVersionReader("x-api-version"), // Ejemplo: Header → x-api-version: 1.0
        new QueryStringApiVersionReader("api-version") // Ejemplo: ?api-version=1.0
    );
});

// Configuración de los servicios
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

// Configuración de los repositorios con Dapper
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

// Configuración de UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();



builder.Services.AddAutoMapper(typeof(MappingProfile));  // Agrega AutoMapper
builder.Services.AddTransient<ISecurityService, SecurityService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();




// Configuración de Dapper Connection Factory
builder.Services.AddScoped<IDbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    return new MySqlConnection(connectionString);
});

// Middleware
builder.Services.AddTransient<ExceptionMiddleware>();

// Configuración de Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    builder.Configuration["Authentication:SecretKey"]
                )
            )
        };
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TallerMecanico API v1");
});



app.UseAuthentication();
app.UseAuthorization();


app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
