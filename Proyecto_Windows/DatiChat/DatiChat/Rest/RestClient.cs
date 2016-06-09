using DatiChat.Clases;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using System.Net.Http;
using System.Windows;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Win32;
using RestSharp;
using System.IO;
using System.Net.Mime;

namespace DatiChat.Rest
{
    public static class RestClient
    {

        public static Task<string> GetService(string url)
        {
            //http://stackoverflow.com/questions/24131067/deserialize-json-to-array-or-list-with-httpclient-readasasync-using-net-4-0-ta
            string urlS = "http://localhost:8191/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlS);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;
            try
            {   
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                    //FIXME
                }
                else
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    return jsonString;
                }
            
            }
            catch (Exception)
            {
                throw new Exception ("error en al intentar conectarse al rest");
            }
        }

        public static bool PostServiceMessages(int from, int to, string text)
        {
            string urlS = "http://localhost:8191/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlS);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
         
            Mensaje mss = new Mensaje() { User_from = from, User_to = to, Message = text};
            var response = client.PostAsJsonAsync("rest/messages", mss);

            //var Json = JsonConvert.SerializeObject(mss);
          //  HttpContent content = new FormUrlEncodedContent(postData);
            //var response = client.PostAsJsonAsync("rest/messages", Json);
            //var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(new { foo = "bar" });
            /*var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("from", from.ToString()));
            postData.Add(new KeyValuePair<string, string>("to", to.ToString()));
            postData.Add(new KeyValuePair<string, string>("text", text));
            */
            //var response = client.PostAsJsonAsync("rest/messages", content).ContinueWith((postTask) => { postTask.Result.EnsureSuccessStatusCode(); });
            response.Wait();
            if( response.IsCompleted )
            {
                return true;
            }else
            {
                return false;
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

        public static List<Mensaje> GetMessageList(int user_from, int user_to)
        {
            string url = "rest/messages/" + user_from.ToString() + "/" + user_to.ToString();
            var jsonString = GetService(url);
            if (jsonString != null)
            {
                List<Mensaje> messages = JsonConvert.DeserializeObject<List<Mensaje>>(jsonString.Result);
                return messages;
            }
            else
            {
                return new List<Mensaje>();
            }
        }
  
        public static void PostFile(int user_from, int user_to, OpenFileDialog arr)
        {
            string urlS = "http://localhost:8191/";
            var client = new RestSharp.RestClient(urlS);
            var request = new RestSharp.RestRequest("rest/files/" + user_from.ToString() + "/" + user_to.ToString());
            request.AddFile("file", arr.FileName);
            request.Method = Method.POST;
            var result = client.ExecuteAsync(request, (response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Enviado Exitosamente");
                }
                else
                {
                    MessageBox.Show("Error en el envío del archivo");
                }
            });

        }

        public static List<Archivo> GetFile(int user_from, int user_to)
        {
            string urlS = "http://localhost:8191/";
            var client = new RestSharp.RestClient(urlS);
            var request = new RestSharp.RestRequest("rest/shared_files/" + user_from.ToString() + "/" + user_to.ToString());
            request.Method = Method.GET;
            var arch = client.DownloadData(request);
            string result = System.Text.Encoding.UTF8.GetString(arch);

            if( arch.Length == 0)
            {
                return null;
            }
            else
            {
                List<Archivo> archivosList = JsonConvert.DeserializeObject<List<Archivo>>(result);
                return archivosList;
            }

        }

        public static byte[] GetFilebyId(int file_id)
        {
            string urlS = "http://localhost:8191/";
            var client = new RestSharp.RestClient(urlS);
            var request = new RestSharp.RestRequest("rest/files/" + file_id.ToString());
            request.Method = Method.GET;
            var arch = client.DownloadData(request);
            return arch;
        }
    }
    
}
