
using Avalonia.Controls;
using Avalonia.Interactivity;
using CatalogApp.Domain.Entities;
using CatalogApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogApp.Views;

public partial class DriversControl : UserControl
{
    public DriversControl()
    {
        InitializeComponent();
    }

    private void Refresh_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is DriversViewModel viewModel)
        {
            viewModel.LoadDrivers();
        }
    }

    private async void Add_Click(object? sender, RoutedEventArgs e)
    {
        AddDriverViewModel viewModel =
            App.Services.GetRequiredService<AddDriverViewModel>();

        AddDriverWindow window = new AddDriverWindow(viewModel);

        Window? owner = TopLevel.GetTopLevel(this) as Window;

        if (owner == null)
        {
            return;
        }

        bool? result = await window.ShowDialog<bool?>(owner);

        if (result == true && DataContext is DriversViewModel driversViewModel)
        {
            driversViewModel.LoadDrivers();
        }
    }

    private async void Edit_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not DriversViewModel driversViewModel ||
            driversViewModel.SelectedDriver == null)
        {
            return;
        }

        AddDriverViewModel viewModel =
            App.Services.GetRequiredService<AddDriverViewModel>();

        viewModel.LoadDriver(driversViewModel.SelectedDriver);

        AddDriverWindow window = new AddDriverWindow(viewModel);

        Window? owner = TopLevel.GetTopLevel(this) as Window;

        if (owner == null)
        {
            return;
        }

        bool? result = await window.ShowDialog<bool?>(owner);

        if (result == true)
        {
            driversViewModel.LoadDrivers();
        }
    }


    private async void Delete_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not DriversViewModel viewModel ||
            viewModel.SelectedDriver == null)
        {
            return;
        }

        Driver driver = viewModel.SelectedDriver;

        Window? owner = TopLevel.GetTopLevel(this) as Window;

        if (owner == null)
        {
            return;
        }

        DeleteDriverWindow confirmation =
            new DeleteDriverWindow(driver);

        bool? confirmed =
            await confirmation.ShowDialog<bool?>(owner);

        if (confirmed == true)
        {
            viewModel.DeleteDriver(driver.Id);
        }
    }

}
