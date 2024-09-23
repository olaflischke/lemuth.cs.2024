using ChinookDal.Model;
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
using System.Windows.Shapes;

namespace ChinookUi
{
    /// <summary>
    /// Interaktionslogik für AddEditArtist.xaml
    /// </summary>
    public partial class AddEditArtist : Window
    {
        public AddEditArtist(Artist artist)
        {
            InitializeComponent();

            this.DataContext = artist;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
