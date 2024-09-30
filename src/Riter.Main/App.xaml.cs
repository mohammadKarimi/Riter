using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Riter.Main.Core;
using Riter.Main.ViewModel;

namespace Riter.Main;

/// <summary>
/// Represents the application and its entry point.
/// Configures services, settings, and handles startup events.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Gets the service provider for dependency injection, allowing services
    /// to be resolved throughout the application.
    /// </summary>
    public IServiceProvider ServiceProvider { get; private set; }

    /// <summary>
    /// Gets the application configuration settings, typically loaded from
    /// appsettings or other configuration sources.
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    /// <summary>
    /// Handles application startup logic, such as configuring services
    /// and loading necessary resources when the application starts.
    /// </summary>
    /// <param name="e">Event arguments for the startup event.</param>
    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false, true);

        Configuration = builder.Build();
        AppSettings appSettings = new ();
        Configuration.Bind(AppSettings.Section, appSettings);
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(appSettings);

        ConfigureServices(serviceCollection);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        ServiceProvider = serviceCollection.BuildServiceProvider();
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private static void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<PalleteState>();
        serviceCollection.AddSingleton<PalleteStateViewModel>();
        serviceCollection.AddTransient(typeof(MainWindow));
    }
}
