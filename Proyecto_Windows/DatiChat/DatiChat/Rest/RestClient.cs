using DatiChat.Clases;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using System.Net.Http;
using System.Windows;

namespace DatiChat.Rest
{
    public static class RestClient
    {
        public static List<Contacto> GetContactList(int userId){
            string url = "http://192.168.1.64:8191/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("rest/contacts/" + userId.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                var contacts = response.Content.ReadAsAsync<List<Contacto>>().Result;
                MessageBox.Show(contacts.First().ToString());
                return contacts;
            }
            else
            {
                List<Contacto> failed = new List<Contacto>();
                return failed;
            }
        }

        public static List<Mensajes> GetMessageList(int user_from, int user_to)
        {
            string url = "http://192.168.1.64:8191/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("rest/messages/" + user_from.ToString() + user_to.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                var contacts = response.Content.ReadAsAsync<List<Mensajes>>().Result;
                return contacts;
            }
            else
            {
                List<Mensajes> failed = new List<Mensajes>();
                return failed;
            }
        }

        public static List<Archivo> GetFilesList(int user_from, int user_to)
        {
            string url = "http://192.168.1.64:8191/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("rest/shared_files/" + user_from.ToString() + user_to.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                var contacts = response.Content.ReadAsAsync<List<Archivo>>().Result;
                return contacts;
            }
            else
            {
                List<Archivo> failed = new List<Archivo>();
                return failed;
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

    }
    
}
