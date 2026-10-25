
using Avalonia.Controls;
using Avalonia.Interactivity;
using CatalogApp.Domain.Entities;
using System.IO;

namespace CatalogApp.Views;

public partial class DeleteDriverWindow : Window
{
    public DeleteDriverWindow()
    {
        InitializeComponent();
    }

    public DeleteDriverWindow(Driver driver)
    {
        InitializeComponent();

        DriverInfo.Text =
            $"ФИО: {driver.FullName}\n" +
            $"Номер удостоверения: {driver.LicenseNumber}\n" +
            $"Телефон: {driver.Phone}\n" +
            $"Стаж работы: {driver.ExperienceYears} лет";
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }

    private void Delete_Click(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }
}
