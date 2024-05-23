using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
if (connectionString == null)
{
    Console.WriteLine("Please set your MONGODB_URI");
    Environment.Exit(0);
}


var client = new MongoClient(connectionString);

var forecasts = new Models.Forecast[]{
  new Models.Forecast{
    Temp = 45,
    Description = "We're screwed",
    Windspeed = 60,
  },
  new Models.Forecast{
    Temp = 40,
    Description = "Fewww...",
    Windspeed = 29,
  },
  new Models.Forecast{
    Temp = 95,
    Description = "SO HOT!!!",
    Windspeed = 10,
  },
  new Models.Forecast{
    Temp = -20,
    Description = "What even is this weather",
    Windspeed = 40,
  },
  new Models.Forecast{
    Temp = -200,
    Description = "...",
    Windspeed = 32,
  },
  new Models.Forecast{
    Temp =  75,
    Description = "...",
    Windspeed = 12,
  }
};

var forcastCollection = client.GetDatabase("local").GetCollection<Models.Forecast>("forecasts");


forcastCollection.InsertMany(forecasts);

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
