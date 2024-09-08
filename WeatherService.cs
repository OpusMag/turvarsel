using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JObject> GetWeatherAsync(string location)
    {
        var response = await _httpClient.GetStringAsync($"https://api.example.com/weather?location={location}");
        return JObject.Parse(response);
    }
}