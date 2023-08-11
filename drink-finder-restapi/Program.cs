using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Persistence.Contexts;
using drink_finder_restapi.Persistence.Repositories;
using drink_finder_restapi.Services;
using AutoMapper;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DFDbContext>(options =>
        { 
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); 
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
builder.Services.AddScoped<IDrinkService, DrinkService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(Program));

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
