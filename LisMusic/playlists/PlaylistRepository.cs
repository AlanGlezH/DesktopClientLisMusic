using LisMusic.accounts.domain;
using LisMusic.ApiServices;
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
        private static string idAccount = SingletonSesion.GetSingletonSesion().account.idAccount;

        public static async Task<List<Playlist>> GetPlaylistsOfAccount()
        {
            string path = "/account/" + idAccount + "/playlist";
            List<Playlist> playlists = null;

            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    playlists = await response.Content.ReadAsAsync<List<Playlist>>();
                    return playlists;
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
           
        }

        public static async Task<bool> CreatePlaylistAsync(Playlist playlist)
        {
            string path = "playlist";
            using (HttpResponseMessage response = await ApiServiceWriter.ApiClient.PostAsJsonAsync(path, playlist))
            {

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }

        public static async Task<List<Playlist>> SearchPlaylists(string playlistTitle)
        {
            string path = "playlists/" + playlistTitle;
            List<Playlist> playlists;

            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    playlists = await response.Content.ReadAsAsync<List<Playlist>>();
                    return playlists;
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }
    }
}
