
using Avalonia.Controls;
using CatalogApp.Domain.Entities;
using CatalogApp.Views;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CatalogApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly MachinesViewModel _machinesViewModel;

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

        Window? owner = App.Current?.ApplicationLifetime
            is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop
                ? desktop.MainWindow
                : null;

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
}
