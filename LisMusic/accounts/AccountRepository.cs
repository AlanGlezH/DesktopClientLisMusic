using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using LisMusic.accounts.domain;
using System.Net.Http;
using LisMusic.ApiServices;

namespace LisMusic.accounts
{
    class AccountRepository
    {

        public static async Task<LoginResponse> LoginAccount(LoginRequest loginRequest)
        {
            string path = "/login";
            LoginResponse loginResponse = null;
            using (HttpResponseMessage response = await ApiServiceWriter.ApiClient.PostAsJsonAsync(path, loginRequest))
            {
                if (response.IsSuccessStatusCode)
                {
                    loginResponse = await response.Content.ReadAsAsync<LoginResponse>();
                    return loginResponse;
                }
                else
                {
                    dynamic objError = await response.Content.ReadAsAsync<dynamic>();
                    string message = objError.error;
                    throw new Exception(message);
                }
            }
            
        }
  
        public static async Task<bool> RegisterAccount(Account account)
        {
            string path = "account";
            using (HttpResponseMessage response = await ApiServiceWriter.ApiClient.PostAsJsonAsync(path, account))
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
    }
}