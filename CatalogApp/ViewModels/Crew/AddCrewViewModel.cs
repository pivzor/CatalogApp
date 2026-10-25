
using CatalogApp.Domain;
using CatalogApp.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CatalogApp.ViewModels;

public partial class AddCrewViewModel : ObservableObject
{
    private readonly IBaseRepository<Crew> _crewRepository;
    private readonly IBaseRepository<Machine> _machineRepository;
    private readonly IBaseRepository<Driver> _driverRepository;

    private Crew? _editingCrew;

    public ObservableCollection<Machine> Machines { get; } = new();
    public ObservableCollection<Driver> Drivers { get; } = new();

    [ObservableProperty]
    private Machine? selectedMachine;

    [ObservableProperty]
    private Driver? selectedDriver;

    [ObservableProperty]
    private bool isEditing;

    [ObservableProperty]
    private bool isActive = true;

    public string WindowTitle =>
        IsEditing ? "Редактирование экипажа" : "Добавление экипажа";

    public string FormTitle =>
        IsEditing ? "Редактирование экипажа" : "Новый экипаж";

    public event Action? CrewAdded;
    public event Action<string>? ErrorOccurred;

    public AddCrewViewModel(
        IBaseRepository<Crew> crewRepository,
        IBaseRepository<Machine> machineRepository,
        IBaseRepository<Driver> driverRepository)
    {
        _crewRepository = crewRepository;
        _machineRepository = machineRepository;
        _driverRepository = driverRepository;

        LoadReferenceData();
    }

    private void LoadReferenceData()
    {
        Machines.Clear();
        Drivers.Clear();

        List<Machine> machines = _machineRepository.GetAll();
        List<Driver> drivers = _driverRepository.GetAll();

        foreach (Machine machine in machines)
        {
            Machines.Add(machine);
        }

        foreach (Driver driver in drivers)
        {
            Drivers.Add(driver);
        }
    }

    public void LoadCrew(Crew crew)
    {
        _editingCrew = crew;

        LoadReferenceData();

        SelectedMachine = Machines.FirstOrDefault(
            machine => machine.Id == crew.MachineId);

        SelectedDriver = Drivers.FirstOrDefault(
            driver => driver.Id == crew.DriverID);

        IsEditing = true;
        IsActive = crew.IsActive;

        OnPropertyChanged(nameof(WindowTitle));
        OnPropertyChanged(nameof(FormTitle));
    }

    [RelayCommand]
    private void Save()
    {
        if (SelectedMachine == null)
        {
            ErrorOccurred?.Invoke("Выберите машину.");
            return;
        }

        if (SelectedDriver == null)
        {
            ErrorOccurred?.Invoke("Выберите водителя.");
            return;
        }

        try
        {
            if (_editingCrew == null)
            {
                Crew crew = new Crew
                {
                    MachineId = SelectedMachine.Id,
                    DriverID = SelectedDriver.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = IsActive
                };

                _crewRepository.Add(crew);
            }
            else
            {
                _editingCrew.MachineId = SelectedMachine.Id;
                _editingCrew.DriverID = SelectedDriver.Id;
                _editingCrew.IsActive = IsActive;

                _crewRepository.Update(_editingCrew);
            }

            CrewAdded?.Invoke();
        }
        catch (Exception ex)
        {
            Exception error = ex;

            while (error.InnerException != null)
            {
                error = error.InnerException;
            }

            ErrorOccurred?.Invoke(
                $"Ошибка сохранения экипажа:\n{error.Message}");
        }
    }
}
