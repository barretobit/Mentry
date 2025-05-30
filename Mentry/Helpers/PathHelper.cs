using System.Globalization;
using System.IO;

namespace Mentry.Helpers;

public static class PathHelper
{
    private static readonly string BaseFolder = @"C:\Mentry\Data";
    private static readonly string FileDateFormat = "yyyy-MM-dd";

    static PathHelper()
    {
        Directory.CreateDirectory(BaseFolder);
    }

    /// <summary>
    /// Returns a path like C:\Mentry\Data\2025-06-14.json (for FocusEntry)
    /// </summary>
    public static string GetDailyFilePath(DateTime date)
    {
        string fileName = date.ToString(FileDateFormat, CultureInfo.InvariantCulture) + ".json";
        return Path.Combine(BaseFolder, fileName);
    }

    /// <summary>
    /// Returns a path like C:\Mentry\Data\2025-06-14.note.json (for DailyNote)
    /// </summary>
    public static string GetNoteFilePath(DateOnly date)
    {
        string fileName = date.ToString(FileDateFormat, CultureInfo.InvariantCulture) + ".note.json";
        return Path.Combine(BaseFolder, fileName);
    }

    /// <summary>
    /// Returns today's file path using UTC date (for FocusEntry)
    /// </summary>
    public static string GetTodayFilePath()
    {
        return GetDailyFilePath(DateTime.UtcNow.Date);
    }

    public static string GetBaseFolder() => BaseFolder;
}