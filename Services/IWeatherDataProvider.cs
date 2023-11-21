namespace WebApplication1.Services;

public interface IWeatherDataProvider
{
    Task<string> GetCurrentAsync(double lat, double lon);
}
