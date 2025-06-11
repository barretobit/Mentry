using System.Diagnostics;
using System.Globalization;
using System.IO;
using Mentry.Models;
using Mentry.Services.Interfaces;

namespace Mentry.Services;

public class GitHistoryService : IGitHistoryService
{
    private readonly string _reposBasePath;
    private readonly string _userName;

    public GitHistoryService(string reposBasePath, string userName)
    {
        _reposBasePath = reposBasePath;
        _userName = userName;
    }

    public async Task<List<GitCommitEntry>> GetCommitsAsync(int daysBack)
    {
        var sinceDate = DateTime.Now.Date.AddDays(-daysBack);
        var allCommits = new List<GitCommitEntry>();

        if (!Directory.Exists(_reposBasePath))
            return allCommits;

        var repoDirs = Directory.GetDirectories(_reposBasePath);

        foreach (var repo in repoDirs)
        {
            var gitDir = Path.Combine(repo, ".git");
            if (!Directory.Exists(gitDir)) continue;

            var result = await RunGitLog(repo, sinceDate);
            var commits = ParseCommits(result, Path.GetFileName(repo));
            allCommits.AddRange(commits);
        }

        return allCommits
            .Where(c => c.Author.Equals(_userName, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(c => c.CommitDate)
            .ToList();
    }

    private static async Task<string> RunGitLog(string repoPath, DateTime sinceDate)
    {
        var args = $"log --since=\"{sinceDate:yyyy-MM-dd}\" --pretty=format:\"%H|%an|%ad|%s\" --date=iso";
        var startInfo = new ProcessStartInfo("git", args)
        {
            WorkingDirectory = repoPath,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(startInfo);
        var output = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();

        return output;
    }

    private static List<GitCommitEntry> ParseCommits(string rawLog, string repoName)
    {
        var lines = rawLog.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var list = new List<GitCommitEntry>();

        foreach (var line in lines)
        {
            var parts = line.Split('|');
            if (parts.Length < 4) continue;

            if (!DateTime.TryParse(parts[2], CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var date))
                continue;

            list.Add(new GitCommitEntry
            {
                CommitDate = date,
                Author = parts[1],
                CommitMessage = parts[3],
                RepositoryName = repoName
            });
        }

        return list;
    }
}