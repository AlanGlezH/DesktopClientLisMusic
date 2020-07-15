using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LisMusic.artists.domain
{
    public class Artist
    {
        public string idArtist { get; set; }
        public string name { get; set; }
        public string cover { get; set; }
        public string registerDate { get; set; }
        public string description { get; set; }
        public BitmapImage coverImage { get; set; }       
    }

}
