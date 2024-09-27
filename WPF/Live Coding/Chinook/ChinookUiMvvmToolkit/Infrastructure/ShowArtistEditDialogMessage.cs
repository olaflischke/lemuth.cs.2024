using ChinookDal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookUiMvvmToolkit.Infrastructure
{
    public class ShowArtistEditDialogMessage
    {
        public ShowArtistEditDialogMessage(Artist artist)
        {
            Artist = artist;
        }

        public Artist Artist { get; set; }
    }
}
