using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class WeatherForecastController : BaseApiController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

//TAKE NOTE:
//[HttpGet(Name = "GetWeatherForecast")] - http://localhost:5021/WeatherForecast - //by default ang name ng endpoint is "ControllerName". 
//[HttpGet("GetWeatherForecast")] http://localhost:5021/WeatherForecast/GetWeatherForecast - //Kapag inindicate sa GET/POST method ung endpoint. yun ang magiging URL under parin ng ControllerName.
