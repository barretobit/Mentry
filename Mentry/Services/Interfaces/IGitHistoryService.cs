using Mentry.Models;

namespace Mentry.Services.Interfaces;

public interface IGitHistoryService
{
    Task<List<GitCommitEntry>> GetCommitsAsync(int daysBack);
}