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
            List<Contacto> Contacts = RestClient.GetContactList(1);

            /*
            Contacts.Add(new Contacto(1,"john@doe-family.com", "John Doe"));
            Contacts.Add(new Contacto(2, "john@doe-family.com2", "John Doe2"));
            Contacts.Add(new Contacto(3, "john@doe-family.com3", "John Doe3"));
            */
            Usuarios.ItemsSource = Contacts;

        }
    }
}
