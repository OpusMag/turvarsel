using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class FavoritesService
{
    private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "favorites.json");

    public async Task<List<string>> GetFavoritesAsync()
    {
        if (!File.Exists(_filePath))
            return new List<string>();

        var json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<string>>(json);
    }

    public async Task AddFavoriteAsync(string location)
    {
        var favorites = await GetFavoritesAsync();
        if (!favorites.Contains(location))
        {
            favorites.Add(location);
            var json = JsonSerializer.Serialize(favorites);
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}