using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mentry.Models;
using Mentry.Services;

namespace Mentry.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IStorageService _storage;

    public MainViewModel(IStorageService storage)
    {
        _storage = storage;
        LoadTodayAsync();
    }

    [ObservableProperty]
    private string newTaskDescription = string.Empty;

    public ObservableCollection<FocusTask> TodayTasks { get; } = new();

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

            await _storage.SaveAsync(entry);
        }
    }

    [RelayCommand]
    private void ToggleComplete(FocusTask task)
    {
        task.IsCompleted = !task.IsCompleted;
        OnPropertyChanged(nameof(TodayTasks));
    }
}