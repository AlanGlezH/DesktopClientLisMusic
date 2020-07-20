using LisMusic.ApiServices;
using LisMusic.Media;
using LisMusic.tracks.domain;
using LisMusic.Utils;
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
        private static string idAccount = SingletonSesion.GetSingletonSesion().account.idAccount;
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

        public static async Task<List<Track>> SearchTracks(string trackTitle)
        {
            string path = "tracks/" + trackTitle;
            List<Track> tracks;

            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    tracks = await response.Content.ReadAsAsync<List<Track>>();
                    return tracks;
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }

        public static async Task<List<Track>> GetTrackAccountHistory()
        {
            string path = "account/" + idAccount + "/tracksHistory" ;
            List<Track> tracks;
            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    tracks = await response.Content.ReadAsAsync<List<Track>>();
                    return tracks;
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }

        public static async Task<List<Track>> GetRadioTrack(Track track)
        {
            string path = "/radio/gender/" + track.album.musicGender.idMusicGender;
            List<Track> tracks;
            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    tracks = await response.Content.ReadAsAsync<List<Track>>();
                    foreach (var item in tracks)
                    {
                        item.album.coverImage = await MediaRepository.GetImage(item.album.cover, "albums");
                    }
                    return tracks;
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }

        public static async void AddPlayToTrack(string idAccount, string  idTrack)
        {
            string path = "/trackPlay/account/" + idAccount + "/track/" + idTrack;
            using (HttpResponseMessage response = await ApiServiceWriter.ApiClient.PostAsync(path, null))
            {
                if (response.IsSuccessStatusCode)
                {

                    Console.WriteLine("Reproducction added");
                }
                else
                {
                    try
                    {
                        dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                        string message = objError.error;
                        Console.WriteLine(message);

                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
