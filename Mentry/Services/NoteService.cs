using System.IO;
using System.Text.Json;
using Mentry.Helpers;
using Mentry.Models;
using Mentry.Services.Interfaces;

namespace Mentry.Services;

public class NoteService : INoteService
{
    public async Task SaveNoteAsync(DailyNote note)
    {
        var filePath = PathHelper.GetNoteFilePath(note.Date);
        var json = JsonSerializer.Serialize(note, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<DailyNote?> LoadByDateAsync(DateTime date)
    {
        var filePath = PathHelper.GetNoteFilePath(DateOnly.FromDateTime(date));

        if (!File.Exists(filePath))
            return null;

        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<DailyNote>(json);
    }
}