using DatiChat.Clases;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using System.Net.Http;
using System.Windows;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DatiChat.Rest
{
    public static class RestClient
    {

        public static Task<string> GetService(string url)
        {
            //http://stackoverflow.com/questions/24131067/deserialize-json-to-array-or-list-with-httpclient-readasasync-using-net-4-0-ta
            string urlS = "http://192.168.250.71:8191/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlS);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return null;//new List<Contacto>();
                    //FIXME
                }
                else
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    return jsonString;
                }
            
            }
            catch (Exception e)
            {
                throw new Exception ("error en al intentar conectarse al rest");
            }
        }

        public static List<Contacto> GetContactList(int userId){
            string url = "rest/contacts/" + userId.ToString();
            var jsonString = GetService(url);
            if (jsonString != null)
            {
                List<Contacto> contacts = JsonConvert.DeserializeObject<List<Contacto>>(jsonString.Result);
                return contacts;
            }
            else
            {
                return new List<Contacto>();
            }
        }

        public static List<Mensajes> GetMessageList(int user_from, int user_to)
        {
            string url = "rest/messages/" + user_from.ToString() + user_to.ToString();
            var jsonString = GetService(url);
            if (jsonString != null)
            {
                List<Mensajes> contacts = JsonConvert.DeserializeObject<List<Mensajes>>(jsonString.Result);
                return contacts;
            }
            else
            {
                return new List<Mensajes>();
            }
        }
        
        /*
        public static List<Archivo> GetFilesList(int user_from, int user_to)
        {
            string url = "rest/shared_files/" + user_from.ToString() + user_to.ToString();
            var jsonString = GetService(url);
            if (jsonString != null)
            {
                List<Archivo> contacts = JsonConvert.DeserializeObject<List<Archivo>>(jsonString.Result);
                return contacts;
            }
            else
            {
                return new List<Archivo>();
            }
        }

        
        public static Archivo GetFile(int file_id)
        {
            string url = "http://192.168.1.64:8191/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("rest/files/" + file_id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                var contacts = response.Content.ReadAsAsync<Archivo>().Result;
                return contacts;
            }
            else
            {
                Archivo failed = new Archivo();
                return failed;
            }
        }
        */
    }
    
}
