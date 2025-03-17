using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using System;
using NLog.Web;
using NLog;
var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
try
{
    logger.Info("Application is starting...");
    var builder = WebApplication.CreateBuilder(args);

    // Database Connection
    builder.Services.AddDbContext<AddressContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("MySqlDatabase"), new MySqlServerVersion(new Version(8, 0, 41))));

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Host.UseNLog();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AddressBookValidator>(); // Ensure Validator is correctly registered


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application startup failed.");
throw;
}
finally
{
    LogManager.Shutdown();
}