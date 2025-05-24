using System.IO;
using System.Text.Json;
using Mentry.Models;
using Mentry.Services.Interfaces;

namespace Mentry.Services;

public class NoteService : INoteService
{
    private readonly string _folder = Path.Combine(AppContext.BaseDirectory, "notes");

    public async Task SaveNoteAsync(DailyNote note)
    {
        Directory.CreateDirectory(_folder);
        string filePath = Path.Combine(_folder, $"{note.Date:yyyy-MM-dd}.json");
        string json = JsonSerializer.Serialize(note, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<DailyNote?> LoadNoteAsync(DateOnly date)
    {
        string filePath = Path.Combine(_folder, $"{date:yyyy-MM-dd}.json");
        if (!File.Exists(filePath)) return null;

        string json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<DailyNote>(json);
    }
}