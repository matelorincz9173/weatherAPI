using WeatherAPI;

namespace WebApplication1.Services;

public interface IJsonProcessor
{
    WeatherForecast Process(string data);    
}