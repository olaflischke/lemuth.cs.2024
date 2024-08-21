using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel.Syndication;
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
using System.Xml;

namespace RssReaderUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _titleText;
        private string _description;
        private string _logoUrl;
        private string _url = "https://www.spiegel.de/schlagzeilen/tops/index.rss";
        private List<SyndicationItem> _items;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<SyndicationItem> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public string TitleText
        {
            get => _titleText;
            set
            {
                _titleText = value;
                OnPropertyChanged();
            }
        }


        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public string LogoUrl
        {
            get
            {
                return _logoUrl;
            }
            set
            {
                _logoUrl = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
            this.DataContext = this;

            //XmlReader xmlReader = XmlReader.Create("https://www.insuedthueringen.de/topmeldung.rss2.feed");
        }


        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // .NET 4.8 und früher
            //Process.Start(e.Uri.AbsoluteUri);

            // .NET (Core)
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = e.Uri.AbsoluteUri
            };

            Process.Start(startInfo);
        }

        // URL-Property fürs Binding
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }
        private void btnLaden_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XmlReader xmlReader = XmlReader.Create(this.Url); //(txtUrl.Text);

                SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

                this.Items = feed.Items.OrderByDescending(el => el.PublishDate).ToList();
                this.TitleText = feed.Title.Text;
                this.Description = feed.Description.Text;
                this.LogoUrl = feed.ImageUrl.ToString();

                //// DataContext zurücksetzen
                //this.DataContext = null;
                //this.DataContext = this;

            }
            catch (Exception ex)
            {
                MessageBox.Show(messageBoxText: $"Fehler beim Laden des Feeds:\n\r{ex.Message}",
                    caption: "Fehler beim Laden",
                    button: MessageBoxButton.OK,
                    icon: MessageBoxImage.Error);
            }
        }
    }
}