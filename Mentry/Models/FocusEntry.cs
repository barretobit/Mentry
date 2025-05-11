namespace Mentry.Models;

public class FocusEntry
{
    public DateTime Date { get; set; } = DateTime.Today;
    public List<FocusTask> Tasks { get; set; } = new();
}

public class FocusTask
{
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}