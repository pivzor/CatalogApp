
using Avalonia.Controls;
using Avalonia.Interactivity;
using CatalogApp.Domain.Entities;
using CatalogApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogApp.Views;

public partial class CrewsControl : UserControl
{
    public CrewsControl()
    {
        InitializeComponent();
    }

    private void Refresh_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is CrewsViewModel viewModel)
        {
            viewModel.LoadCrews();
        }
    }

    private async void Add_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not CrewsViewModel crewsViewModel)
        {
            return;
        }

        AddCrewViewModel viewModel =
            App.Services.GetRequiredService<AddCrewViewModel>();

        AddCrewWindow window = new AddCrewWindow(viewModel);
        Window? owner = TopLevel.GetTopLevel(this) as Window;

        if (owner == null)
        {
            return;
        }

        bool? result = await window.ShowDialog<bool?>(owner);

        if (result == true)
        {
            crewsViewModel.LoadCrews();
        }
    }

    private async void Edit_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not CrewsViewModel crewsViewModel ||
            crewsViewModel.SelectedCrew == null)
        {
            return;
        }

        AddCrewViewModel viewModel =
            App.Services.GetRequiredService<AddCrewViewModel>();

        viewModel.LoadCrew(crewsViewModel.SelectedCrew);

        AddCrewWindow window = new AddCrewWindow(viewModel);
        Window? owner = TopLevel.GetTopLevel(this) as Window;

        if (owner == null)
        {
            return;
        }

        bool? result = await window.ShowDialog<bool?>(owner);

        if (result == true)
        {
            crewsViewModel.LoadCrews();
        }
    }

    private async void Delete_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not CrewsViewModel viewModel ||
            viewModel.SelectedCrew == null)
        {
            return;
        }

        Crew crew = viewModel.SelectedCrew;

        DeleteCrewWindow confirmation =
            new DeleteCrewWindow(crew);

        Window? owner = TopLevel.GetTopLevel(this) as Window;

        if (owner == null)
        {
            return;
        }

        bool? confirmed =
            await confirmation.ShowDialog<bool?>(owner);

        if (confirmed == true)
        {
            viewModel.DeleteCrew(crew.Id);
        }
    }
}
