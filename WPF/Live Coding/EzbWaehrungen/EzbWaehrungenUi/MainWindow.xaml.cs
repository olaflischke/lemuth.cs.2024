using EzbWaehrungenDal;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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

            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);

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

        private void btnNeueWaehrung_Click(object sender, RoutedEventArgs e)
        {
            Waehrung waehrnung = new Waehrung();

            // Dialog instanziieren und aufrufen
            AddEditWaehrung addWaehrung = new AddEditWaehrung(waehrnung);
            // ShowDialog gibt das DialogResult zurück (siehe Button_Click in AddEditWaehrung.xaml.cs)
            if (addWaehrung.ShowDialog() == true)
            {
                // Neue Währung dem gewählten Handelstag hinzufügen
                Handelstag handelstag = lbxHandelstage.SelectedItem as Handelstag;
                handelstag.Waehrungen.Add(waehrnung);
            }

        }
    }
}