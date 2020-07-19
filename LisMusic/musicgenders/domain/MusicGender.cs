using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.musicgenders.domain
{
    public class MusicGender
    {
        public int idMusicGender { get; set; }
        public string genderName { get; set; }

        public MusicGender(int idMusicGender, string genderName)
        {
            this.idMusicGender = idMusicGender;
            this.genderName = genderName;
        }

        public MusicGender() { }


    }
}
