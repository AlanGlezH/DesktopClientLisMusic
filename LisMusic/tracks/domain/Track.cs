using LisMusic.albums.domain;
using LisMusic.musicgenders.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.tracks.domain
{
    public class Track
    {
        public string idTrack { get; set; }
        public string title { get; set; }
        public int reproductions { get; set; }
        public string fileTrack { get; set; }
        public bool avaible { get; set; }
        public int indexRow { get; set; }
        public string duration { get; set; }
        public string albumTitle { get; set; }
        public string artistName { get; set; }
        public Album album { get; set; }

        public Track(string idTrack, string title, double duration, int reproductions, string fileTrack, bool avaible, MusicGender musicGender, Album album)
        {
            this.idTrack = idTrack;
            this.title = title;
            this.reproductions = reproductions; TimeSpan result = TimeSpan.FromSeconds(duration);
            this.duration = result.ToString("mm':'ss"); ;
            this.fileTrack = fileTrack;
            this.avaible = avaible;
            this.album = album;
        }
    }
}
