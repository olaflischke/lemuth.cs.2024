using ChinookDal.Model;
using Microsoft.EntityFrameworkCore;
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

namespace ChinookUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Chinook;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChinookContext context = GetContext(noTracking: true, Console.WriteLine);

            List<Genre> qGenres = context.Genres.ToList();

            foreach (Genre genre in qGenres)
            {
                TreeViewItem treeViewItem = new TreeViewItem() { Header = genre.Name, Tag = genre };
                treeViewItem.Items.Add(new TreeViewItem());
                treeViewItem.Expanded += this.TreeViewItem_Expanded;
                tviArtists.Items.Add(treeViewItem);
            }
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            if (sender is TreeViewItem aktuell)
            {
                aktuell.Items.Clear();

                if (aktuell.Tag is Genre genre)
                {
                    // Alle Artists dieses Genres
                    ChinookContext context = GetContext(noTracking: false, Console.WriteLine);
                    IQueryable<Artist> qArtists = context.Tracks
                                                        .Include(tr => tr.Album)
                                                        .ThenInclude(al => al.Artist)//.AsTracking() 
                                                        .Where(tr => tr.Genre.GenreId == genre.GenreId)
                                                        .Select(tr => tr.Album.Artist).Distinct();

                    var alben = context.Albums.ToList();
                    var tracks = context.Tracks.ToList();


                    foreach (Artist item in qArtists)
                    {
                        TreeViewItem tviArtist = new TreeViewItem() { Header = item.Name, Tag = item };
                        aktuell.Items.Add(tviArtist);
                    }
                }
            }
        }

        private ChinookContext GetContext(bool noTracking = false, Action<string> logAction = null)
        {
            DbContextOptionsBuilder<ChinookContext> builder = new DbContextOptionsBuilder<ChinookContext>()
                                                                    .UseSqlServer(connectionString);

            if (noTracking)
            {
                builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            else
            {
                builder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            }

            if (logAction != null)
            {
                builder.LogTo(logAction, Microsoft.Extensions.Logging.LogLevel.Information);
            }

            return new ChinookContext(builder.Options);
        }
    }

}