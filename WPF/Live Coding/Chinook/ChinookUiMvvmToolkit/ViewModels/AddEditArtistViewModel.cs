using ChinookDal.Model;
using ChinookUiMvvmToolkit.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChinookUiMvvmToolkit.ViewModels
{
    public class AddEditArtistViewModel : ObservableObject
    {
        public AddEditArtistViewModel(Artist artist, IMessenger messenger)
        {
            this.Artist = artist;
            this.messenger = messenger;
            this.SaveCommand = new RelayCommand(SaveArtist);
        }

        private void SaveArtist()
        {
            messenger.Send(new ArtistChangedMessage());
            CloseAction();
        }

        public Action CloseAction { get; set; }

        private Artist _artist;

        public Artist Artist
        {
            get { return _artist; }
            set { SetProperty(ref _artist, value); }
        }

        public ICommand SaveCommand;
        private readonly IMessenger messenger;
    }
}
