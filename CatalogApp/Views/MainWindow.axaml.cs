
using Avalonia.Controls;
using CatalogApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CatalogApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();


        DriversControl.DataContext =
            App.Services.GetRequiredService<DriversViewModel>();

        CrewsControl.DataContext =
            App.Services.GetRequiredService<CrewsViewModel>();

        Icon = new Avalonia.Controls.WindowIcon(
            Avalonia.Platform.AssetLoader.Open(
                new Uri("avares://CatalogApp/Assets/icon.ico")));
    }
}
