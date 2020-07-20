using LisMusic.ApiServices;
using LisMusic.artists.domain;
using LisMusic.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.artists
{
    class ArtistRepository
    {
        private static string idAccount = SingletonSesion.GetSingletonSesion().account.idAccount;
        public static async Task<List<Artist>> GetArtistsOfAccount()
        {
            string path = "account/" + idAccount + "/artistsLike";
            List<Artist> artists;

            using(HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    artists = await response.Content.ReadAsAsync<List<Artist>>();
                    return artists;
                }else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }

        public static async Task<List<Artist>> SearchArtist(string artistName)
        {
            string path = "artists/" + artistName;
            List<Artist> artists;

            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    artists = await response.Content.ReadAsAsync<List<Artist>>();
                    return artists;
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }


        public static async Task<bool> CreateArtist(Artist artist)
        {
            string path = "artist";
            Artist newArtist;
            using (HttpResponseMessage response = await ApiServiceWriter.ApiClient.PostAsJsonAsync(path, artist))
            {
                if (response.IsSuccessStatusCode)
                {
                    newArtist = await response.Content.ReadAsAsync<Artist>();
                    if(newArtist != null)
                    {
                        SingletonArtist.SetSinglentonArtist(newArtist);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }

        public static async Task<bool> GetArtistOfAccount(string idAccount)
        {
            string path = "account/" + idAccount + "/artist";
            Artist newArtist;
            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    newArtist = await response.Content.ReadAsAsync<Artist>();
                    if (newArtist != null)
                    {
                        SingletonArtist.SetSinglentonArtist(newArtist);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
