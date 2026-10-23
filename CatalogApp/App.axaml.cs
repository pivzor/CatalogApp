
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Catalog.DAL;
using CatalogApp.Domain;
using CatalogApp.ViewModels;
using CatalogApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogApp;

public partial class App : Application
{
    public static ServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ServiceCollection services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                "Host=localhost;Port=5432;Database=db;Username=postgres;Password=12345678"));

        services.AddTransient(
            typeof(IBaseRepository<>),
            typeof(BaseRepository<>));

        services.AddTransient<MachinesViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<AddMachineViewModel>();

        Services = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
