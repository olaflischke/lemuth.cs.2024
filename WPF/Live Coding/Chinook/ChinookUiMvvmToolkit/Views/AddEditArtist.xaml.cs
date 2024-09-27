using ChinookDal.Model;
using ChinookUiMvvmToolkit.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace ChinookUiMvvmToolkit.Views;

/// <summary>
/// Interaktionslogik für AddEditArtist.xaml
/// </summary>
public partial class AddEditArtist : Window
{
    private readonly IMessenger messenger;

    public AddEditArtist(Artist artist, IMessenger messenger)
    {
        InitializeComponent();

        this.messenger = messenger;
        
        var viewModel = new AddEditArtistViewModel(artist, messenger);
        viewModel.CloseAction = () =>
        {
            this.DialogResult = true;
            this.Close();
        };

        this.DataContext = viewModel;

    }
}
