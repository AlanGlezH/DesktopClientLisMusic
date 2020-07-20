
using LisMusic.ApiServices;
using LisMusic.personaltracks.domain;
using LisMusic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.personaltracks
{
    class PersonalTrackRepository
    {
        private static string idAccount = SingletonSesion.GetSingletonSesion().account.idAccount;

        public static async Task<List<PersonalTrack>> GetPersonsalTracksAccount()
        {
            List<PersonalTrack> personalTracks = null;
            string path = "account/" + idAccount + "/personalTracks";

            using (HttpResponseMessage response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode)
                {
                    personalTracks = await response.Content.ReadAsAsync<List<PersonalTrack>>();
                    return personalTracks;
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
        }

        public static async Task<PersonalTrack> CreatePersonalTrack(PersonalTrack personalTrack)
        {
            string path = "personalTrack";


            using (HttpResponseMessage response = await ApiServiceWriter.ApiClient.PostAsJsonAsync(path,personalTrack))
            {
                if (response.IsSuccessStatusCode)
                {
                    var newPersonalTrack = await response.Content.ReadAsAsync<PersonalTrack>();
                    return newPersonalTrack;
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
