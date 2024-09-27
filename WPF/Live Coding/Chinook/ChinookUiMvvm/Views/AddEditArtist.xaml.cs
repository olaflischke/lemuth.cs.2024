using ChinookDal.Model;
using System.Windows;

namespace ChinookUiMvvm.Views;

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
