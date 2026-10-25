using Avalonia.Controls;
using CatalogApp.Domain.Entities;
using CatalogApp.ViewModels.Machin;
using CatalogApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CatalogApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly MachinesViewModel _machinesViewModel;

    [ObservableProperty]
    private Machine? selectedMachine;

    public ObservableCollection<Machine> Machines =>
        _machinesViewModel.Machines;

    public MainViewModel(MachinesViewModel machinesViewModel)
    {
        _machinesViewModel = machinesViewModel;
    }

    [RelayCommand]
    private void Refresh()
    {
        _machinesViewModel.LoadMachines();
    }

    [RelayCommand]
    private async Task AddMachine()
    {
        AddMachineViewModel viewModel =
            App.Services.GetRequiredService<AddMachineViewModel>();

        AddMachineWindow window = new AddMachineWindow(viewModel);

        Window? owner = GetMainWindow();

        if (owner == null)
        {
            return;
        }

        bool? result = await window.ShowDialog<bool?>(owner);

        if (result == true)
        {
            _machinesViewModel.LoadMachines();
        }
    }

    [RelayCommand]
    private async Task EditMachine()
    {
        if (SelectedMachine == null)
        {
            return;
        }

        AddMachineViewModel viewModel =
            App.Services.GetRequiredService<AddMachineViewModel>();

        viewModel.LoadMachine(SelectedMachine);

        AddMachineWindow window = new AddMachineWindow(viewModel);

        Window? owner = GetMainWindow();

        if (owner == null)
        {
            return;
        }

        bool? result = await window.ShowDialog<bool?>(owner);

        if (result == true)
        {
            _machinesViewModel.LoadMachines();
        }
    }

    [RelayCommand]
    private async Task DeleteMachine()
    {
        if (SelectedMachine == null)
        {
            return;
        }

        Machine machine = SelectedMachine;

        Window? owner = GetMainWindow();

        if (owner == null)
        {
            return;
        }

        DeleteMachineWindow confirmation =
            new DeleteMachineWindow(machine);

        bool? confirmed = await confirmation.ShowDialog<bool?>(owner);

        if (confirmed != true)
        {
            return;
        }

        _machinesViewModel.DeleteMachine(machine.Id);
        SelectedMachine = null;
    }

    private static Window? GetMainWindow()
    {
        if (App.Current?.ApplicationLifetime
            is Avalonia.Controls.ApplicationLifetimes
                .IClassicDesktopStyleApplicationLifetime desktop)
        {
            return desktop.MainWindow;
        }

        return null;
    }
}