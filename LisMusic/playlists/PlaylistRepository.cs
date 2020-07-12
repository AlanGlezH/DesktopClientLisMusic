using LisMusic.accounts.domain;
using LisMusic.playlists.domain;
using LisMusic.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.playlists
{
    class PlaylistRepository
    {
        private static string url = "http://localhost:6000";
        private static string idAccount = SingletonSesion.GetSingletonSesion().account.idAccount;

        public static List<Playlist> GetPlaylistsOfAccount()
        {
            WebRequest webRequest = WebRequest.Create(url + "/account/"+ idAccount +"/playlist");
            List<Playlist> playlists = null;

            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (WebException ex)
            {
                var error = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().Trim();
                dynamic errorObj = JsonConvert.DeserializeObject(error);
                String messageFromServer = errorObj.error;

                throw new Exception(messageFromServer, ex);
            }

            using (var streamReader = new StreamReader(webResponse.GetResponseStream()))
            {
                String result = streamReader.ReadToEnd().Trim();
                playlists = JsonConvert.DeserializeObject<List<Playlist>>(result);
            }
            return playlists;




        }
    }
}
