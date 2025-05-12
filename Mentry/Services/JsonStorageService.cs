using System.IO;
using System.Text.Json;
using Mentry.Models;

namespace Mentry.Services;

public class JsonStorageService : IStorageService
{
    private const string FolderPath = "Data";

    private static string GetFilePath(DateTime date) => Path.Combine(FolderPath, $"{date:yyyy-MM-dd}.json");

    public async Task SaveAsync(FocusEntry entry)
    {
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        var json = JsonSerializer.Serialize(entry, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(GetFilePath(entry.Date), json);
    }

    public async Task<FocusEntry?> LoadAsync(DateTime date)
    {
        var file = GetFilePath(date);
        if (!File.Exists(file)) return null;

        var json = await File.ReadAllTextAsync(file);
        return JsonSerializer.Deserialize<FocusEntry>(json);
    }
}