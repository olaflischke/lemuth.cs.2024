using ChinookDal.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
            using ChinookContext context = GetContext(noTracking: true, Console.WriteLine);

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
                FillArtistsForGenre(aktuell);
            }
        }

        private void FillArtistsForGenre(TreeViewItem genreItem)
        {
            genreItem.Items.Clear();

            if (genreItem.Tag is Genre genre)
            {
                // Alle Artists dieses Genres
                using ChinookContext context = GetContext(noTracking: false, Console.WriteLine);
                IQueryable<Artist> qArtists = context.Tracks
                                                    .Include(tr => tr.Album)
                                                    .ThenInclude(al => al.Artist)//.AsTracking() 
                                                    .Where(tr => tr.Genre.GenreId == genre.GenreId)
                                                    .Select(tr => tr.Album.Artist).Distinct();

                var alben = context.Albums.ToList();
                var tracks = context.Tracks.ToList();


                foreach (Artist item in qArtists.OrderBy(a => a.Name))
                {
                    TreeViewItem tviArtist = new TreeViewItem() { Header = item.Name, Tag = item };
                    genreItem.Items.Add(tviArtist);
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

        // Bestehenden Artist ändern
        private void btnEditArtist_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selected = (TreeViewItem)tviArtists.SelectedItem;

            if (selected.Tag is Artist artist)
            {
                AddEditArtist dlgEditArtist = new AddEditArtist(artist);
                if (dlgEditArtist.ShowDialog() == true)
                {
                    using ChinookContext context = GetContext(true, Console.WriteLine);
                    // Geht, aber ungeschickt, wg. Abhängigkeiten
                    //context.Attach(artist);
                    //context.Entry(artist).State = EntityState.Modified;

                    context.Artists.Update(artist); // State: Modified, abhängige Elemente kriegen je nach Zustand Modified, Added oder Unchanged

                    context.SaveChanges(); // Änderung speichern

                    FillArtistsForGenre((TreeViewItem)selected.Parent);
                }
            }
        }

        private void btnAddArtist_Click(object sender, RoutedEventArgs e)
        {
            Artist artist = new Artist();
            AddEditArtist dlgAddArtist = new AddEditArtist(artist);

            if (dlgAddArtist.ShowDialog() == true)
            {
                using ChinookContext context = GetContext(true, Console.WriteLine);

                context.Artists.Add(artist);

                context.SaveChanges();
            }
        }
    }

}