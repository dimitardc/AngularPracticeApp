using Microsoft.OpenApi.Models;
using AngularApp1.Server.Data;
using AngularApp1.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

//ASKAI to explain the entities n shit in this file

var builder = WebApplication.CreateBuilder(args);

//Add db context

//this is old inmemory db
//builder.Services.AddDbContext<Entities>(options => 
//    options.UseInMemoryDatabase(databaseName: "Flights"),ServiceLifetime.Singleton);

//sql db
builder.Services.AddDbContext<Entities>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("Flights")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=>
{
    c.AddServer(new OpenApiServer
    {
        Description = "Development server",
        Url = "https://localhost:5001"          
    });

    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]+ e.ActionDescriptor.RouteValues["controller"]}");
});

//adds an instance of the entity class
//useful when we dont want to make multiple instances for different controllers
//and want to just use one --IN INMEMORY DB--
//builder.Services.AddSingleton<Entities>();

//an instance is created every time theres a http requestMicrosoft.Data.SqlClient.SqlException: 'A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)'

builder.Services.AddScoped<Entities>();

var app = builder.Build();

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();
entities.Database.EnsureCreated();

var _random = new Random();

if (!entities.Flights.Any())
{
    Flight[] flightsToSeed = new Flight[]
{

    new Flight(
        Guid.NewGuid(),
        "American Airlines",
        _random.Next(90, 5000).ToString(),
        new TimePlace("Los Angeles", DateTime.Now.AddHours(_random.Next(1, 3))),
        new TimePlace("Istanbul", DateTime.Now.AddHours(_random.Next(4, 10))),
        _random.Next(1, 853)
    ),
    new Flight(
        Guid.NewGuid(),
        "Deutsche BA",
        _random.Next(90, 5000).ToString(),
        new TimePlace("Munchen", DateTime.Now.AddHours(_random.Next(1, 10))),
        new TimePlace("Schiphol", DateTime.Now.AddHours(_random.Next(4, 15))),
        _random.Next(1, 853)
    ),
    new Flight(
        Guid.NewGuid(),
        "British Airways",
        _random.Next(90, 5000).ToString(),
        new TimePlace("London, England", DateTime.Now.AddHours(_random.Next(1, 15))),
        new TimePlace("Vizzola-Ticino", DateTime.Now.AddHours(_random.Next(4, 18))),
        _random.Next(1, 853)
    ),
    new Flight(
        Guid.NewGuid(),
    "Basiq Air",
        _random.Next(90, 5000).ToString(),
        new TimePlace("Amsterdam", DateTime.Now.AddHours(_random.Next(1, 21))),
        new TimePlace("Glasgow, Scotland", DateTime.Now.AddHours(_random.Next(4, 21))),
        _random.Next(1, 853)
    ),
    new Flight(
        Guid.NewGuid(),
        "BB Heliag",
        _random.Next(90, 5000).ToString(),
        new TimePlace("Zurich", DateTime.Now.AddHours(_random.Next(1, 23))),
        new TimePlace("Baku", DateTime.Now.AddHours(_random.Next(4, 25))),
        _random.Next(1, 853)
    ),
    new Flight(
        Guid.NewGuid(),
        "Adria Airways",
        _random.Next(90, 5000).ToString(),
        new TimePlace("Ljubljana", DateTime.Now.AddHours(_random.Next(1, 15))),
        new TimePlace("Warsaw", DateTime.Now.AddHours(_random.Next(4, 19))),
        _random.Next(1, 853)
    ),
    new Flight(
        Guid.NewGuid(),
        "ABA Air",
        _random.Next(90, 5000).ToString(),
        new TimePlace("Praha Ruzyne", DateTime.Now.AddHours(_random.Next(1, 55))),
        new TimePlace("Paris", DateTime.Now.AddHours(_random.Next(4, 58))),
        _random.Next(1, 853)
    ),
    new Flight(
        Guid.NewGuid(),
        "AB Corporate Aviation",
        _random.Next(90, 5000).ToString(),
        new TimePlace("Le Bourget", DateTime.Now.AddHours(_random.Next(1, 58))),
        new TimePlace("Zagreb", DateTime.Now.AddHours(_random.Next(4, 60))),
        _random.Next(1, 853)
    )
};

    entities.Flights.AddRange(flightsToSeed);
    entities.SaveChanges();
}



app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
