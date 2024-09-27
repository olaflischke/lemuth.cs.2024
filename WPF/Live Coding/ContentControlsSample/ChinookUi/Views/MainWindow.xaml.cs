using ChinookUi.ViewModels;
using System.Windows;

namespace ChinookUi.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        MainWindowViewModel viewModel = new MainWindowViewModel();

        //viewModel.CloseAction = () => { this.Close(); };
        viewModel.CloseAction = FensterSchliessen;

        this.DataContext = viewModel;
    }

    private void FensterSchliessen()
    {
        MessageBoxResult result = MessageBox.Show(messageBoxText: "Wirklich schliessen?",
            caption: "Programm beenden",
            button: MessageBoxButton.YesNo,
            icon: MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            this.Close();
        }
    }
}