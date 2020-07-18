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
        public string artist_name { get; set; }
        public string albumTitle { get; set; }
        public string artistName { get; set; }



        public Track(string idTrack, string title, int reproductions, string fileTrack, bool avaible, string artist_name)
        {
            this.idTrack = idTrack;
            this.title = title;
            this.reproductions = reproductions;
            this.fileTrack = fileTrack;
            this.avaible = avaible;
            this.artist_name = artist_name;
        }
    }
}
