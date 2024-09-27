using ChinookDal.Model;
using ChinookUiMvvmToolkit.Infrastructure;
using ChinookUiMvvmToolkit.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChinookUiMvvmToolkit.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ILogger logger = new FileLogger(@"c:\tmp\lemuth.log");

            this.DataContext = App.Current.Services.GetService(typeof(MainWindowViewModel));
        }

        private void tviArtists_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MainWindowViewModel? viewModel = this.DataContext as MainWindowViewModel;

            if (viewModel != null && e.NewValue is Artist selected)
            {
                viewModel.SelectedArtist = selected;
            }
        }

    }
}