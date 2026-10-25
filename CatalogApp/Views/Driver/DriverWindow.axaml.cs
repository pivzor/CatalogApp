
using Avalonia.Controls;
using Avalonia.Interactivity;
using CatalogApp.Domain.Entities;
using System;

namespace CatalogApp.Views;

public partial class DriverWindow : Window
{
    public Driver? Driver { get; private set; }

    public DriverWindow(Driver? driver = null)
    {
        InitializeComponent();

        if (driver == null)
        {
            WindowTitle.Text = "Добавление водителя";
            ExperienceYearsInput.Value = 0;
            return;
        }

        Driver = driver;

        WindowTitle.Text = "Редактирование водителя";
        FullNameTextBox.Text = driver.FullName;
        LicenseNumberTextBox.Text = driver.LicenseNumber;
        PhoneTextBox.Text = driver.Phone;
        ExperienceYearsInput.Value = driver.ExperienceYears;
    }

    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        string fullName = FullNameTextBox.Text?.Trim() ?? "";
        string licenseNumber = LicenseNumberTextBox.Text?.Trim() ?? "";
        string phone = PhoneTextBox.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(fullName) ||
            string.IsNullOrWhiteSpace(licenseNumber) ||
            string.IsNullOrWhiteSpace(phone))
        {
            WindowTitle.Text = "Заполните все поля!";
            return;
        }

        int experienceYears = Convert.ToInt32(
            ExperienceYearsInput.Value ?? 0);

        Driver ??= new Driver();

        Driver.FullName = fullName;
        Driver.LicenseNumber = licenseNumber;
        Driver.Phone = phone;
        Driver.ExperienceYears = experienceYears;

        Close(Driver);
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close(null);
    }
}
