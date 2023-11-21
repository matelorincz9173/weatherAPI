using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using WeatherAPI;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherDataProvider _weatherDataProvider;
    private readonly IJsonProcessor _jsonProcessor;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IWeatherDataProvider weatherDataProvider, IJsonProcessor jsonProcessor)
    {
        _logger = logger;
        _weatherDataProvider = weatherDataProvider;
        _jsonProcessor = jsonProcessor;
    }

    [HttpGet("GetWeatherForecast"), Authorize(Roles = "Admin")]
    public IActionResult Get(DateTime date)
    {
        if (date.Year < 2023)
        {
            return NotFound("Invalid date. Please provide a date before 2023.");
        }

        var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = date.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

        return Ok(forecast);
    }

    [HttpGet("GetCurrent"), Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<WeatherForecast>> GetCurrent()
    {
        var lat = 47.497913;
        var lon = 19.040236;

        try
        {
            var weatherData = await _weatherDataProvider.GetCurrentAsync(lat, lon);
            return Ok(_jsonProcessor.Process(weatherData));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting weather data");
            return NotFound("Error getting weather data");
        }
    }
    
}