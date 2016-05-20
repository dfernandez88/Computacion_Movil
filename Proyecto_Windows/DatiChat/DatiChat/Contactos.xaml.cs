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
    /// <summary>
    /// Interaction logic for Contactos.xaml
    /// </summary>
    public partial class Contactos : Window
    {
        public Contactos()
        {
            InitializeComponent();
            try
            {
                //Cambiar el id (obtenerlo del id)
                List<Contacto> Contacts = RestClient.GetContactList(2);
                Usuarios.ItemsSource = Contacts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //FIXME
            }
        }
    }
}
