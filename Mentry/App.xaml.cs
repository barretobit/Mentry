using System.IO;
using System.Windows;
using Mentry.Models;
using Mentry.Services;
using Mentry.Services.Interfaces;
using Mentry.ViewModels;
using Mentry.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public partial class App : Application
{
    public new static App Current => (App)Application.Current;
    public IServiceProvider Services { get; private set; }

    public App()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration();

        var gitSettings = new GitSettings();
        configuration.GetSection("Git").Bind(gitSettings);
        services.AddSingleton(gitSettings);

        ConfigureServices(services);
        Services = services.BuildServiceProvider();
    }

    private IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IStorageService, StorageService>();
        services.AddSingleton<INoteService, NoteService>();
        services.AddSingleton<IGitHistoryService, GitHistoryService>();

        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }
}