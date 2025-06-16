﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mentry.Models;
using Mentry.Services.Interfaces;

namespace Mentry.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IStorageService _storageService;
    private readonly INoteService _noteService;

    public MainViewModel(IStorageService storageService, INoteService noteService)
    {
        _storageService = storageService;
        _noteService = noteService;

        // fire and forget (safe here)
        LoadTodayAsync();
    }

    [ObservableProperty]
    private string newTaskDescription = string.Empty;

    [ObservableProperty]
    private string? dailyNote;

    public ObservableCollection<FocusTask> TodayTasks { get; } = new();

    private async void LoadTodayAsync()
    {
        // Load focus tasks
        var entry = await _storageService.LoadAsync(DateTime.Today);
        if (entry?.Tasks is { Count: > 0 })
        {
            TodayTasks.Clear();
            foreach (var task in entry.Tasks)
                TodayTasks.Add(task);
        }

        // Load daily note
        var note = await _noteService.LoadByDateAsync(DateTime.Today);
        DailyNote = note?.Note ?? string.Empty;
    }

    [RelayCommand]
    private async Task AddTaskAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewTaskDescription) && TodayTasks.Count < 3)
        {
            TodayTasks.Add(new FocusTask { Description = NewTaskDescription });
            NewTaskDescription = string.Empty;

            await SaveFocusTasksAsync();
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
        await SaveFocusTasksAsync();
        OnPropertyChanged(nameof(TodayTasks));
    }

    private async Task SaveFocusTasksAsync()
    {
        var entry = new FocusEntry
        {
            Date = DateTime.Today,
            Tasks = TodayTasks.ToList()
        };

        await _storageService.SaveAsync(entry);
    }
}