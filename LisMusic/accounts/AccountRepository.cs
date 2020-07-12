using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using LisMusic.accounts.resources;
using LisMusic.accounts.domain;

namespace LisMusic.accounts
{
    class AccountRepository
    {

        private static string url = "http://localhost:5000";
        public static LoginResponse LoginAccount(LoginRequest loginRequest)
        {
            
            WebRequest webRequest = WebRequest.Create(url + "/login");
            webRequest.Method = "post";
            webRequest.ContentType = "application/json;charset-UTF-8";
            LoginResponse loginResponse = null;

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(loginRequest);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (WebException ex )
            {
                var error = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().Trim();
                dynamic errorObj = JsonConvert.DeserializeObject(error);
                String messageFromServer = errorObj.error;

                throw new Exception(messageFromServer, ex);
            }
                               
            using (var streamReader = new StreamReader(webResponse.GetResponseStream()))
            {
                String result = streamReader.ReadToEnd().Trim();
                loginResponse = JsonConvert.DeserializeObject<LoginResponse>(result);
            }
            return loginResponse;
        }
  
        public static bool RegisterAccount(Account account)
        {
            WebRequest webRequest = WebRequest.Create(url + "/account");
            webRequest.Method = "post";
            webRequest.ContentType = "application/json;charset-UTF-8";
            
            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(account);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                var error = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().Trim();
                dynamic errorObj = JsonConvert.DeserializeObject(error);
                String messageFromServer = errorObj.error;
                throw new Exception(messageFromServer,ex);
            }

           
        }
    }
}