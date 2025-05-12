using Mentry.Models;

namespace Mentry.Services;

public interface IStorageService
{
    Task SaveAsync(FocusEntry entry);

    Task<FocusEntry?> LoadAsync(DateTime date);
}