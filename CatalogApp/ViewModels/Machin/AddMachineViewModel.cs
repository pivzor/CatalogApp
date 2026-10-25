using CatalogApp.Domain;
using CatalogApp.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace CatalogApp.ViewModels;

public partial class AddMachineViewModel : ObservableObject
{
    private readonly IBaseRepository<Machine> _repository;
    private Machine? _editingMachine;

    [ObservableProperty]
    private string brand = string.Empty;

    [ObservableProperty]
    private string model = string.Empty;

    [ObservableProperty]
    private string number = string.Empty;

    [ObservableProperty]
    private int year = DateTime.Now.Year;

    [ObservableProperty]
    private bool isEditing;

    public string WindowTitle =>
        IsEditing ? "Редактирование машины" : "Добавление машины";

    public string FormTitle =>
        IsEditing ? "Редактирование машины" : "Новая машина";

    public event Action? MachineAdded;
    public event Action<string>? ErrorOccurred;

    public AddMachineViewModel(IBaseRepository<Machine> repository)
    {
        _repository = repository;
    }

    public void LoadMachine(Machine machine)
    {
        _editingMachine = machine;

        Brand = machine.Brand;
        Model = machine.Model;
        Number = machine.Number;
        Year = machine.Year;

        IsEditing = true;
        OnPropertyChanged(nameof(WindowTitle));
        OnPropertyChanged(nameof(FormTitle));
    }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(Brand) ||
            string.IsNullOrWhiteSpace(Model) ||
            string.IsNullOrWhiteSpace(Number))
        {
            ErrorOccurred?.Invoke("Заполните все поля.");
            return;
        }

        if (Year < 1900 || Year > 2100)
        {
            ErrorOccurred?.Invoke("Укажите корректный год выпуска.");
            return;
        }

        try
        {
            if (_editingMachine == null)
            {
                Machine machine = new Machine
                {
                    Brand = Brand.Trim(),
                    Model = Model.Trim(),
                    Number = Number.Trim(),
                    Year = Year
                };

                _repository.Add(machine);
            }
            else
            {
                _editingMachine.Brand = Brand.Trim();
                _editingMachine.Model = Model.Trim();
                _editingMachine.Number = Number.Trim();
                _editingMachine.Year = Year;

                _repository.Update(_editingMachine);
            }

            MachineAdded?.Invoke();
        }
        catch (Exception ex)
        {
            Exception error = ex;

            while (error.InnerException != null)
            {
                error = error.InnerException;
            }

            ErrorOccurred?.Invoke(
                $"Ошибка сохранения машины:\n{error.Message}");
        }
    }
}