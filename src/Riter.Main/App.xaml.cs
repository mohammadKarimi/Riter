using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Riter.Main.Core;
using Riter.Main.ViewModel;

namespace Riter.Main;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }
    public IConfiguration Configuration { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

        Configuration = builder.Build();
        AppSettings appSettings = new();
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

