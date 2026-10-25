
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using CatalogApp.ViewModels;

namespace CatalogApp.Views;

public partial class AddDriverWindow : Window
{
    public AddDriverWindow()
    {
        InitializeComponent();
    }

    public AddDriverWindow(AddDriverViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;

        viewModel.DriverAdded += () =>
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
                Margin = new Thickness(16),
                Spacing = 12,
                Children =
                {
                    new TextBlock
                    {
                        Text = message,
                        TextWrapping = TextWrapping.Wrap
                    },
                    new Button
                    {
                        Content = "ОК",
                        HorizontalAlignment = HorizontalAlignment.Right
                    }
                }
            }
        };

        Button button =
            (Button)((StackPanel)errorWindow.Content!).Children[1];

        button.Click += (_, _) => errorWindow.Close();

        await errorWindow.ShowDialog(this);
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}
