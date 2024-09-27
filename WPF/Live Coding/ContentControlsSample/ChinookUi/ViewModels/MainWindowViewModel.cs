using ChinookDal.Model;
using ChinookUi.Views;
using System.Windows.Controls;

namespace ChinookUi.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel()
    {
        this.ShowArtistBrowserCommand = new RelayCommand(a => ShowArtistsBrowser());
        this.ShowCustomerBrowserCommand = new RelayCommand(a => ShowCustomerBrowser());
        this.ExitCommand = new RelayCommand(a => Exit());

        this.View = new CustomerBrowser();
    }

    private void Exit()
    {
        CloseAction();
    }

    private void ShowCustomerBrowser()
    {
        this.View = new CustomerBrowser();
    }

    private void ShowArtistsBrowser()
    {
        this.View = new ArtistsBrowser();
    }

    private UserControl _view;

    public UserControl View
    {
        get { return _view; }
        set
        {
            _view = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ShowCustomerBrowserCommand { get; set; }
    public RelayCommand ShowArtistBrowserCommand { get; set; }
    public RelayCommand ExitCommand { get; set; }

    // Delegat für das Schliessen des Fensters
    public Action CloseAction { get; set; }
}
