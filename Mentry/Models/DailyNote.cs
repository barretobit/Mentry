namespace Mentry.Models;

public class DailyNote
{
    public DateOnly Date { get; set; }
    public string Note { get; set; } = string.Empty;
}