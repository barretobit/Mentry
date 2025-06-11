namespace Mentry.Models;

public class GitCommitEntry
{
    public DateTime CommitDate { get; set; }
    public string CommitMessage { get; set; } = string.Empty;
    public string RepositoryName { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
}