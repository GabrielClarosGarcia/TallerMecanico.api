using System.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TallerMecanico.Api.Mapping;
using TallerMecanico.Api.Middleware;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Infrastructure.Data;
using TallerMecanico.Infrastructure.Repositories;
using TallerMecanico.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TallerMecanico API v1");
});



app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();
