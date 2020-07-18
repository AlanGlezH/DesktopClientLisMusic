using LisMusic.albums.domain;
using LisMusic.ApiServices;
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

namespace LisMusic.albums
{
    class AlbumRepository
    {
        private static string idAccount = SingletonSesion.GetSingletonSesion().account.idAccount;
        public static async Task<List<Album>> GetAlbumsLikeOfAccount()
        {
            List<Album> albums = null;
            string path = "account/" + idAccount + "/albumsLike";

            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    albums = await response.Content.ReadAsAsync<List<Album>>();
                    return albums;
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }

        public static async Task<List<Album>> GetArtistAlbums(string idArtist)
        {
            string path = "artist/" + idArtist + "/album";
            List<Album> albums;
            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    albums = await response.Content.ReadAsAsync<List<Album>>();
                    return albums;
                } else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }

        public static async Task<List<Album>> SearchAlbums(string albumName)
        {
            string path = "albums/" + albumName;
            List<Album> albums;

            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    albums = await response.Content.ReadAsAsync<List<Album>>();
                    return albums;
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
