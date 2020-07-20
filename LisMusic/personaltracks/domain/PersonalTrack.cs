using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.personaltracks.domain
{
    class PersonalTrack
    {
        public String idPersonalTrack { get; set; }
        public String idAccount { get; set; }
        public String title { get; set; }
        public String gender { get; set; }
        public String album { get; set; }
        public String duration { get; set; }
        public String fileTrack { get; set; }
        public bool avaialable { get; set; } 
        public int indexRow { get; set; }

        public PersonalTrack(string idPersonalTrack, string idAccount, string title, string gender, string album, int duration, string fileTrack, bool avaialable, int indexRow)
        {
            this.idPersonalTrack = idPersonalTrack;
            this.idAccount = idAccount;
            this.title = title;
            this.gender = gender;
            this.album = album;
            TimeSpan result = TimeSpan.FromSeconds(duration);
            this.duration = result.ToString("mm':'ss");
            this.fileTrack = fileTrack;
            this.avaialable = avaialable;
            this.indexRow = indexRow;
        }

    }
}

