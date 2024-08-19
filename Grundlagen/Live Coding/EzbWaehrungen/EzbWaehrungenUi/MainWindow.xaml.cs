using EzbWaehrungenDal;
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

namespace EzbWaehrungenUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";
                Archiv archiv = new Archiv(url);

                this.DataContext = archiv;
            }
            catch (EzbWaehrungenDalException ex)
            {
                MessageBox.Show(caption: "Fehler beim Datenlesen",
                    messageBoxText: $"Keine Daten lesbar!\n\r{ex.Message}\n\r{ex.InnerException?.Message}",
                    icon: MessageBoxImage.Exclamation, button: MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Start.");
            }
        }
    }
}