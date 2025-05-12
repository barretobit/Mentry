using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mentry.Models;

namespace Mentry.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string newTaskDescription = string.Empty;

    public ObservableCollection<FocusTask> TodayTasks { get; } = new();

    [RelayCommand]
    private void AddTask()
    {
        if (!string.IsNullOrWhiteSpace(NewTaskDescription) && TodayTasks.Count < 3)
        {
            TodayTasks.Add(new FocusTask { Description = NewTaskDescription });
            NewTaskDescription = string.Empty;
        }
    }

    [RelayCommand]
    private void ToggleComplete(FocusTask task)
    {
        task.IsCompleted = !task.IsCompleted;
        OnPropertyChanged(nameof(TodayTasks));
    }
}