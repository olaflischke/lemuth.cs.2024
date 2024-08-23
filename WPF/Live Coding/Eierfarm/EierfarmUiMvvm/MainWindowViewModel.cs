using EierfarmBl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EierfarmUiMvvm;

public class MainWindowViewModel : INotifyPropertyChanged
{

    public MainWindowViewModel()
    {
        this.NeuesHuhnCommand = new RelayCommand(a => NeuesHuhn());
        this.NeueGansCommand = new RelayCommand(a => NeueGans());
        this.FuetternCommand = new RelayCommand(a => TierFuettern(), p => CanFuettern());
        this.EiLegenCommand = new RelayCommand(a => TierEilegen(), p => CanEiLegen());
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool CanEiLegen()
    {
        return (this.SelectedTier != null);
    }

    private void TierEilegen()
    {
        this.SelectedTier.EiLegen();
    }

    private bool CanFuettern()
    {
        return (this.SelectedTier != null);
        //if (this.SelectedTier!=null)
        //{
        //    return true;
        //}

        //return false;
    }

    private void TierFuettern()
    {
        if (this.SelectedTier is Gefluegel gefluegel)
        {
            gefluegel.Fressen();
        }
    }

    private void NeueGans()
    {
        Gans gans = new Gans();

        this.Tiere.Add(gans);
        this.SelectedTier = gans;
    }

    private void NeuesHuhn()
    {
        Huhn huhn = new Huhn("Neues Huhn");

        this.Tiere.Add(huhn);
        this.SelectedTier = huhn;
    }

    public RelayCommand NeuesHuhnCommand { get; set; }
    public RelayCommand NeueGansCommand { get; set; }
    public RelayCommand FuetternCommand { get; set; }
    public RelayCommand EiLegenCommand { get; set; }

    public ObservableCollection<IEiLeger> Tiere { get; set; } = new ObservableCollection<IEiLeger>();

    private IEiLeger selectedTier;
    public IEiLeger SelectedTier
    {
        get => selectedTier;
        set
        {
            selectedTier = value;
            OnPropertyChanged();
        }
    }


}
