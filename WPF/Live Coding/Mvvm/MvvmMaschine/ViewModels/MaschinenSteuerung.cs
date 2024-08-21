using Maschinen;
using MvvmMaschine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmMaschine.ViewModels;

public class MaschinenSteuerung
{
    public MaschinenSteuerung()
    {
        this.Maschine = new TennisballWurfmaschine();

        this.StartCommand = new RelayCommand( a => Starten(),p => CanStarten());
        this.StopCommand = new RelayCommand(a => Stoppen(), p => CanStoppen());
    }

    private bool CanStoppen()
    {
        return this.Maschine.IstAmLaufen;
    }

    private void Stoppen()
    {
        this.Maschine.Stopp();
    }

    private bool CanStarten()
    {
        return !this.Maschine.IstAmLaufen;
    }

    private void Starten()
    {
        this.Maschine.Start();
    }

    public TennisballWurfmaschine Maschine { get; set; }
    public RelayCommand StartCommand { get; set; }
    public RelayCommand StopCommand { get; set; }

}
