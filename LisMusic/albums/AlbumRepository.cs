using LisMusic.albums.domain;
using LisMusic.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.albums
{
    class AlbumRepository
    {
        private static string url = "http://localhost:6000";
        private static string idAccount = SingletonSesion.GetSingletonSesion().account.idAccount;
        private static string token = SingletonSesion.GetSingletonSesion().access_token;

        public static List<Album> GetAlbumsLikeOfAccount()
        {
            WebRequest webRequest = WebRequest.Create(url + "/account/" + idAccount + "/albumsLike");
            webRequest.Headers.Add("Authorization", token);
            List<Album> albums = null;
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
                albums = JsonConvert.DeserializeObject<List<Album>>(result);
            }

            return albums;


        }

    }
}
