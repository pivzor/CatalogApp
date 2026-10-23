
using CatalogApp.Domain;
using CatalogApp.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace CatalogApp.ViewModels;

public partial class AddMachineViewModel : ObservableObject
{
    private readonly IBaseRepository<Machine> _repository;

    [ObservableProperty]
    private string brand = string.Empty;

    [ObservableProperty]
    private string model = string.Empty;

    [ObservableProperty]
    private string number = string.Empty;

    [ObservableProperty]
    private int year = DateTime.Now.Year;

    public event Action? MachineAdded;

    public event Action<string>? ErrorOccurred;

    public AddMachineViewModel(IBaseRepository<Machine> repository)
    {
        _repository = repository;
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
            Machine machine = new Machine
            {
                Brand = Brand.Trim(),
                Model = Model.Trim(),
                Number = Number.Trim(),
                Year = Year
            };

            _repository.Add(machine);
            MachineAdded?.Invoke();
        }
        catch (Exception)
        {
            ErrorOccurred?.Invoke(
                "Не удалось сохранить машину. Проверьте данные и подключение к базе.");
        }
    }
}
