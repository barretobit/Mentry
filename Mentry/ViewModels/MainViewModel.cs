using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mentry.Models;
using Mentry.Services;
using Mentry.Services.Interfaces;

namespace Mentry.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IStorageService _storageService;
    private readonly INoteService _noteService;

    public MainViewModel(IStorageService storage, INoteService note)
    {
        _storageService = storage;
        _noteService = note;
        LoadTodayAsync();
    }

    [ObservableProperty]
    private string newTaskDescription = string.Empty;

    [ObservableProperty]
    private string? dailyNote;

    public ObservableCollection<FocusTask> TodayTasks { get; } = new();

    public async Task LoadNoteAsync()
    {
        var note = await _noteService.LoadNoteAsync(DateOnly.FromDateTime(DateTime.Today));
        DailyNote = note?.Note ?? string.Empty;
    }

    [RelayCommand]
    private async Task AddTaskAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewTaskDescription) && TodayTasks.Count < 3)
        {
            TodayTasks.Add(new FocusTask { Description = NewTaskDescription });
            NewTaskDescription = string.Empty;

            var entry = new FocusEntry
            {
                Date = DateTime.Today,
                Tasks = TodayTasks.ToList()
            };

            await _storageService.SaveAsync(entry);
        }
    }

    [RelayCommand]
    public async Task SaveNoteAsync()
    {
        var note = new DailyNote
        {
            Date = DateOnly.FromDateTime(DateTime.Today),
            Note = DailyNote ?? string.Empty
        };
        await _noteService.SaveNoteAsync(note);
    }

    [RelayCommand]
    private async Task ToggleCompleteAsync(FocusTask task)
    {
        task.IsCompleted = !task.IsCompleted;

        var entry = new FocusEntry
        {
            Date = DateTime.Today,
            Tasks = TodayTasks.ToList()
        };

        await _storageService.SaveAsync(entry);
        OnPropertyChanged(nameof(TodayTasks));
    }

    private async void LoadTodayAsync()
    {
        var entry = await _storageService.LoadAsync(DateTime.Today);
        if (entry?.Tasks != null)
        {
            foreach (var task in entry.Tasks)
            {
                TodayTasks.Add(task);
            }
        }
    }
}