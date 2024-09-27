
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using UseContentControl.Views;

namespace UseContentControl.ViewModels;

public class ViewController : BaseViewModel
{
    private UserControl _view;

    public ViewController()
    {
        this.ShowView1Command = new RelayCommand(a => ShowView1());
        this.ShowView2Command = new RelayCommand(a => ShowView2());

        
    }

    private void ShowView1()
    {
        this.View = new View1();
    }

    private void ShowView2()
    {
        this.View = new View2();
    }

    public RelayCommand ShowView1Command { get; set; }
    public RelayCommand ShowView2Command { get; set; }

    public UserControl View
    {
        get
        {
            return _view;
        }
        set
        {
            _view = value;
            OnPropertyChanged();
        }
    }

}
