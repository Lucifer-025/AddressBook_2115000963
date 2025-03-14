using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using System;

var builder = WebApplication.CreateBuilder(args);

// Database Connection
builder.Services.AddDbContext<AddressContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("MySqlDatabase"), new MySqlServerVersion(new Version(8, 0, 41))));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();