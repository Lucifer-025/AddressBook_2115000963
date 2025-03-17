using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using System;
using NLog.Web;
var builder = WebApplication.CreateBuilder(args);

// Database Connection
builder.Services.AddDbContext<AddressContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("MySqlDatabase"), new MySqlServerVersion(new Version(8, 0, 41))));

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Host.UseNLog();
builder.Services.AddSwaggerGen();


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