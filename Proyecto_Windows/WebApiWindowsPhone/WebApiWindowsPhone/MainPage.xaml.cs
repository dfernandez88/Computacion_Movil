using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WebApiWindowsPhone.Resources;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using PortableClassLibrary;

namespace WebApiWindowsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }


        public async Task<HttpResponseMessage> obtenerContactos() 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://Juan-Desktop:30303/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                 return await client.GetAsync("api/contactos/");
            }
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage response = await obtenerContactos();

            if (response.IsSuccessStatusCode)
            {
                String responseContent = await response.Content.ReadAsStringAsync();
                List<Contacto> contactos = JsonConvert.DeserializeObject<List<Contacto>>(responseContent);
                ContactosList.ItemsSource = contactos;
            }
        }
    }
}