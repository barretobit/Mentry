using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Mentry.Services;
using Mentry.ViewModels;
using Mentry.Views;
using Mentry.Services.Interfaces;

namespace Mentry;

public partial class App : Application
{
    public new static App Current => (App)Application.Current;
    public IServiceProvider Services { get; private set; }

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        Services = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Register your services
        services.AddSingleton<IStorageService, StorageService>();
        services.AddSingleton<INoteService, NoteService>();

        // ViewModels
        services.AddTransient<MainViewModel>();

        // Views
        services.AddTransient<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}