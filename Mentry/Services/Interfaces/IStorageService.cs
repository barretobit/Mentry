using Mentry.Models;

namespace Mentry.Services.Interfaces;

public interface IStorageService
{
    Task SaveAsync(FocusEntry entry);

    Task<FocusEntry?> LoadAsync(DateTime date);
}