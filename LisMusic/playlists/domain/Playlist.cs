using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LisMusic.playlists.domain
{
    public class Playlist
    {
        public int idPlaylist { get; set; }
        public string title { get; set; }
        public string creation { get; set; }
        public string cover { get; set; }
        public bool publicPlaylist { get; set; }
        public int idPlaylistType { get; set; }
        public string idAccount { get; set; }
        public string owner { get; set; }
    


        public Playlist(int idPlaylist, string title, string creation, string cover, bool publicPlaylist,
                        int idPlaylistType, string idAccount, string owner)
        {
            this.idPlaylist = idPlaylist;
            this.title = title;
            this.creation = creation;
            this.cover = cover;
            this.publicPlaylist = publicPlaylist;
            this.idPlaylistType = idPlaylistType;
            this.idAccount = idAccount;
            this.owner = owner;
        }
    }
}
