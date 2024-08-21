﻿using System.Diagnostics;
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
    public partial class MainWindow : Window
    {
        public List<SyndicationItem> Items { get; set; }
        public string TitleText { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }

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
        public string Url { get; set; } = "https://www.spiegel.de/schlagzeilen/tops/index.rss";

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