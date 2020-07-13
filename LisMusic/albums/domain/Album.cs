using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.albums.domain
{
    public class Album
    {
        public string idAlbum { get; set; }
        public string title { get; set; }
        public string cover { get; set; }
        public string publication { get; set; }
        public string recordCompany { get; set; }
        public int idMusicGender { get; set; }
        public int idAlbumType { get; set; }
        public string artistName { get; set; }
    }
  
}
