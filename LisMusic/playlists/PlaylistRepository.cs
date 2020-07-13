using LisMusic.accounts.domain;
using LisMusic.playlists.domain;
using LisMusic.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.playlists
{
    class PlaylistRepository
    {
        private static string url = "http://localhost:6000";
        private static string urlWriter = "http://localhost:5000";
        private static string idAccount = SingletonSesion.GetSingletonSesion().account.idAccount;
        private static string token = SingletonSesion.GetSingletonSesion().access_token;

        public static async Task<List<Playlist>> GetPlaylistsOfAccount()
        {
            WebRequest webRequest = WebRequest.Create(url + "/account/"+ idAccount +"/playlist");
            List<Playlist> playlists = null;

            WebResponse webResponse;
            try
            {
                webResponse = await webRequest.GetResponseAsync();
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

        public static bool CreatePlaylist(Playlist playlist)
        {
            WebRequest webRequest = WebRequest.Create(urlWriter + "/playlist");
            webRequest.Method = "post";
            webRequest.Headers.Add("Authorization", token);
            webRequest.ContentType = "application/json;charset-UTF-8";

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(playlist);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
              
               
                
                return true;
            }
            catch (WebException ex)
            {
                var error = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().Trim();
                dynamic errorObj = JsonConvert.DeserializeObject(error);
                String messageFromServer = errorObj.error;

                throw new Exception(messageFromServer, ex);
            }

            


        }
    }
}
