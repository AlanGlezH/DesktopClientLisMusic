using LisMusic.ApiServices;
using LisMusic.tracks.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.tracks
{
    class TrackRepository
    {
        public static async Task<List<Track>> GetTracksAlbum(string idAlbum)
        {
            string path = "/album/" + idAlbum + "/track";
            List<Track> tracks;

            using(HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    tracks = await response.Content.ReadAsAsync<List<Track>>();
                    return tracks;
                } else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }

        public static async Task<List<Track>> GetTracksPlaylist(int idPlaylist)
        {
            string path = "/playlist/" + idPlaylist + "/tracks";
            List<Track> tracks;

            using(HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    tracks = await response.Content.ReadAsAsync<List<Track>>();
                    return tracks;
                } else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }
    }
}
