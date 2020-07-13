using LisMusic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.ApiServices
{
    class ApiServiceReader
    {
        public static HttpClient ApiClient { get; set; }

        private ApiServiceReader() { }

        public static void Initialize()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("http://localhost:6000/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if(SingletonSesion.GetSingletonSesion() != null)
            {
                ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    SingletonSesion.GetSingletonSesion().access_token);
            }
        }
    }
}
