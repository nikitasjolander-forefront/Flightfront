using Flightfront.ExternalData;
using FlightFront.Application.Services;
using FlightFront.Core.Interfaces;
using FlightFront.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExternalDataServices(builder.Configuration);

builder.Services.AddSingleton<MetarTrimmingService>();
builder.Services.AddSingleton<MetarParserService>();

builder.Services.AddSingleton<IAirportSearchService>(sp =>
    new AirportSearchService(Path.Combine(AppContext.BaseDirectory, "Data", "airports.csv")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
