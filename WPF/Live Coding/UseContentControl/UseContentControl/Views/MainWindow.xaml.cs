using System.Windows;
using UseContentControl.ViewModels;

namespace UseContentControl.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.DataContext = new ViewController();
    }
}