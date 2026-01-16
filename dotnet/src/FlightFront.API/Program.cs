using FlightFront.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CheckWX HttpClient
builder.Services.AddHttpClient<ICheckWxService, CheckWxService>(client =>
{
    var config = builder.Configuration.GetSection("CheckWxApi");
    client.BaseAddress = new Uri(config["BaseUrl"]!);
    client.DefaultRequestHeaders.Add("X-API-Key", config["ApiKey"]);
});

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
