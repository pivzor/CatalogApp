
using Avalonia.Controls;
using Avalonia.Interactivity;
using CatalogApp.Domain.Entities;

namespace CatalogApp.Views;

public partial class DeleteCrewWindow : Window
{
    public DeleteCrewWindow()
    {
        InitializeComponent();
    }

    public DeleteCrewWindow(Crew crew)
    {
        InitializeComponent();

        CrewInfo.Text =
            $"ID экипажа: {crew.Id}\n" +
            $"ID машины: {crew.MachineId}\n" +
            $"ID водителя: {crew.DriverID}\n" +
            $"Дата создания: {crew.CreatedAt:dd.MM.yyyy HH:mm}";
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
