using System.IO;
using System.Text.Json;
using Mentry.Helpers;
using Mentry.Models;
using Mentry.Services.Interfaces;

namespace Mentry.Services;

public class StorageService : IStorageService
{
    public async Task SaveAsync(FocusEntry entry)
    {
        var filePath = PathHelper.GetDailyFilePath(entry.Date);
        var json = JsonSerializer.Serialize(entry, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<FocusEntry?> LoadAsync(DateTime date)
    {
        var filePath = PathHelper.GetDailyFilePath(date);
        if (!File.Exists(filePath))
            return null;

        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<FocusEntry>(json);
    }
}