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
        public string duration { get; set; }
        public int reproductions { get; set; }
        public string fileTrack { get; set; }
        public bool avaible { get; set; }
        public int indexRow { get; set; }



        public Track(string idTrack, string title, double duration, int reproductions, string fileTrack, bool avaible)
        {
            this.idTrack = idTrack;
            this.title = title;
            TimeSpan result = TimeSpan.FromSeconds(duration);
            this.duration = result.ToString("mm':'ss"); ;
            this.reproductions = reproductions;
            this.fileTrack = fileTrack;
            this.avaible = avaible;
        }
    }
}
