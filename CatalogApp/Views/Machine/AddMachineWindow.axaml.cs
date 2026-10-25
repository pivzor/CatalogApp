
using Avalonia.Controls;
using Avalonia.Interactivity;
using CatalogApp.ViewModels;

namespace CatalogApp.Views;

public partial class AddMachineWindow : Window
{
    public AddMachineWindow()
    {
        InitializeComponent();
    }

    public AddMachineWindow(AddMachineViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;

        viewModel.MachineAdded += () =>
        {
            Close(true);
        };

        viewModel.ErrorOccurred += message =>
        {
            ShowError(message);
        };
    }

    private async void ShowError(string message)
    {
        Window errorWindow = new Window
        {
            Title = "Ошибка",
            Width = 340,
            Height = 160,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Content = new StackPanel
            {
                Margin = new Avalonia.Thickness(16),
                Spacing = 12,
                Children =
                {
                    new TextBlock
                    {
                        Text = message,
                        TextWrapping = Avalonia.Media.TextWrapping.Wrap
                    },
                    new Button
                    {
                        Content = "ОК",
                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right
                    }
                }
            }
        };

        Button button = (Button)((StackPanel)errorWindow.Content!).Children[1];
        button.Click += (_, _) => errorWindow.Close();

        await errorWindow.ShowDialog(this);
    }
}
