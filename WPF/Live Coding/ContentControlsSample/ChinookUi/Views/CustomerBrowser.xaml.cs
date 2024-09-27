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
    /// Interaktionslogik für CustomerBrowser.xaml
    /// </summary>
    public partial class CustomerBrowser : UserControl
    {
        public CustomerBrowser()
        {
            InitializeComponent();

            this.DataContext = new CustomerBrowserViewModel();
        }
    }
}
