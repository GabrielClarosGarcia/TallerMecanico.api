using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Infrastructure.Services;
using TallerMecanico.infrastructure.Data;
using TallerMecanico.api.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using TallerMecanico.Api.Mapping;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TallerMecanico.Api.Validation.ClientCreateValidator>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<WorkshopContext>(opts =>
    opts.UseMySql(cs, ServerVersion.AutoDetect(cs)));

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
