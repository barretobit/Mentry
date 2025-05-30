using Mentry.Models;

namespace Mentry.Services.Interfaces;

public interface INoteService
{
    Task SaveNoteAsync(DailyNote note);

    Task<DailyNote?> LoadByDateAsync(DateTime date);
}