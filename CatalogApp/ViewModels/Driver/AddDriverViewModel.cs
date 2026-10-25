
using CatalogApp.Domain;
using CatalogApp.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace CatalogApp.ViewModels;

public partial class AddDriverViewModel : ObservableObject
{
    private readonly IBaseRepository<Driver> _repository;
    private Driver? _editingDriver;

    [ObservableProperty]
    private string fullName = string.Empty;

    [ObservableProperty]
    private string licenseNumber = string.Empty;

    [ObservableProperty]
    private string phone = string.Empty;

    [ObservableProperty]
    private int experienceYears;

    [ObservableProperty]
    private bool isEditing;

    public string WindowTitle =>
        IsEditing ? "Редактирование водителя" : "Добавление водителя";

    public string FormTitle =>
        IsEditing ? "Редактирование водителя" : "Новый водитель";

    public event Action? DriverAdded;
    public event Action<string>? ErrorOccurred;

    public AddDriverViewModel(IBaseRepository<Driver> repository)
    {
        _repository = repository;
    }

    public void LoadDriver(Driver driver)
    {
        _editingDriver = driver;

        FullName = driver.FullName;
        LicenseNumber = driver.LicenseNumber;
        Phone = driver.Phone;
        ExperienceYears = driver.ExperienceYears;

        IsEditing = true;

        OnPropertyChanged(nameof(WindowTitle));
        OnPropertyChanged(nameof(FormTitle));
    }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(FullName) ||
            string.IsNullOrWhiteSpace(LicenseNumber) ||
            string.IsNullOrWhiteSpace(Phone))
        {
            ErrorOccurred?.Invoke("Заполните все поля.");
            return;
        }

        if (ExperienceYears < 0 || ExperienceYears > 70)
        {
            ErrorOccurred?.Invoke("Укажите корректный стаж работы.");
            return;
        }

        try
        {
            if (_editingDriver == null)
            {
                Driver driver = new Driver
                {
                    FullName = FullName.Trim(),
                    LicenseNumber = LicenseNumber.Trim(),
                    Phone = Phone.Trim(),
                    ExperienceYears = ExperienceYears
                };

                _repository.Add(driver);
            }
            else
            {
                _editingDriver.FullName = FullName.Trim();
                _editingDriver.LicenseNumber = LicenseNumber.Trim();
                _editingDriver.Phone = Phone.Trim();
                _editingDriver.ExperienceYears = ExperienceYears;

                _repository.Update(_editingDriver);
            }

            DriverAdded?.Invoke();
        }
        catch (Exception ex)
        {
            Exception error = ex;

            while (error.InnerException != null)
            {
                error = error.InnerException;
            }

            ErrorOccurred?.Invoke(
                $"Ошибка сохранения водителя:\n{error.Message}");
        }
    }
}
