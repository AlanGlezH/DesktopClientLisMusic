using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.ApiServices
{
    class ApiServiceWriter
    {
        public static HttpClient ApiClient { get; set; }

        private ApiServiceWriter() { }

        public static void Initialize()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("http://25.100.75.136:5000/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
