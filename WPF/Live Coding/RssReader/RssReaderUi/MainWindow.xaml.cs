using System.ServiceModel.Syndication;
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
using System.Xml;

namespace RssReaderUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            XmlReader xmlReader = XmlReader.Create("https://www.spiegel.de/schlagzeilen/tops/index.rss");
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

            this.DataContext = feed;
        }
    }
}