using Avalonia.Controls;
using Avalonia.Interactivity;
using CatalogApp.Domain.Entities;

namespace CatalogApp.Views;

public partial class DeleteMachineWindow : Window
{
    public DeleteMachineWindow()
    {
        InitializeComponent();
    }

    public DeleteMachineWindow(Machine machine)
    {
        InitializeComponent();

        MachineInfo.Text =
            $"Марка: {machine.Brand}\n" +
            $"Модель: {machine.Model}\n" +
            $"Государственный номер: {machine.Number}";
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