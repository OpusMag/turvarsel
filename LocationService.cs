using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class LocationService
{
    private readonly HttpClient _httpClient;

    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JArray> SearchLocationsAsync(string query)
    {
        var response = await _httpClient.GetStringAsync($"https://api.example.com/locations?query={query}");
        return JArray.Parse(response);
    }
}