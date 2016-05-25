using DatiChat.Clases;
using DatiChat.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatiChat
{    
    public partial class Contactos : Window
    {
        private  List<Contacto> Contacts { get; set; }
        private Contacto ContactoPpal { get; set; }
        public Contactos()
        {
            InitializeComponent();
            try
            {
                //Cambiar el id (obtenerlo del id)
                this.ContactoPpal = new Contacto(1, "crada", "Juan Camilo Rada");
                this.Contacts = RestClient.GetContactList(ContactoPpal.User_id);
                Usuarios.ItemsSource = Contacts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //FIXME
            }
        }
        
        private void Usuarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //Obtenermos el elemento seleccionado//
                int select = Usuarios.SelectedIndex;
                Contacto value = this.Contacts[select];

                //Generamos la ventana de mensajes entre los dos contactos//
                Mensajes msn = new Mensajes(ContactoPpal, value);
                msn.Owner = this;
                msn.Show();
                this.Hide();
            }
            catch (Exception)
            {

                new Exception("error, no existen contactos");
            }
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
