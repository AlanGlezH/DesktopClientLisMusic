using LisMusic.artists.domain;
using LisMusic.musicgenders.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LisMusic.albums.domain
{
    public class Album
    {
        public string idAlbum { get; set; }
        public string title { get; set; }
        public string cover { get; set; }
        public string publication { get; set; }
        public string recordCompany { get; set; }
        public int idAlbumType { get; set; }
        public MusicGender musicGender { get; set; }
        public Artist artist { get; set; }
        public BitmapImage coverImage { get; set; }

        public Album(string idAlbum, string title, string cover, string publication, string recordCompany, int idAlbumType, BitmapImage coverImage, Artist artist, MusicGender musicGender)
        {
            this.idAlbum = idAlbum;
            this.title = title;
            this.cover = cover;
            this.publication = publication;
            this.recordCompany = recordCompany;
            this.idAlbumType = idAlbumType;
            this.coverImage = coverImage;
            this.artist = artist;
            this.musicGender = musicGender;
        }

        public Album() {
            this.artist = new Artist();
        }
    }
  
}
