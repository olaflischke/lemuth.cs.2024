using ChinookDal.Model;
using ChinookUi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChinookUi.Views
{
    /// <summary>
    /// Interaktionslogik für ArtistsBrowser.xaml
    /// </summary>
    public partial class ArtistsBrowser : UserControl
    {
        public ArtistsBrowser()
        {
            InitializeComponent();
            this.DataContext = new ArtistsBrowserViewModel();
        }

        private void tviArtists_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ArtistsBrowserViewModel? viewModel = this.DataContext as ArtistsBrowserViewModel;

            if (viewModel != null && e.NewValue is Artist selected)
            {
                viewModel.SelectedArtist = selected;
            }
        }
    }
}
