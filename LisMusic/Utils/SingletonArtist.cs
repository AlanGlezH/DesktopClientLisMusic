using LisMusic.artists.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.Utils
{
    class SingletonArtist
    {
        private static Artist artist;

        public static void SetSinglentonArtist(Artist newArtist)
        {
            if(artist == null)
            {
                artist = newArtist;
            }
        }

        public static Artist GetSingletonArtist()
        {
            return artist;
        }

        public static void CleanSingleton()
        {
            artist = null;
        }
    }
}
