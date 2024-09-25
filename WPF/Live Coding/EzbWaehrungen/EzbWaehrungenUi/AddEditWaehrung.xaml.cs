using EzbWaehrungenDal;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EzbWaehrungenUi
{
    /// <summary>
    /// Interaktionslogik für AddEditWaehrung.xaml
    /// </summary>
    public partial class AddEditWaehrung : Window
    {
        public AddEditWaehrung(Waehrung waehrung)
        {
            InitializeComponent();

            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);

            this.DataContext = waehrung;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
