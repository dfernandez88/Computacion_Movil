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
 
    public partial class Mensajes : Window
    {
        private int v1;
        private int v2;
        private int v3;
        private string v4;
        private int v5;

        public Mensajes()
        {
            InitializeComponent();

            List<Mensajes> Contacts = new List<Mensajes>();

            Contacts.Add(new Mensajes(1, 1, 2, "Hola Como Estas",12/28/1995));
            Contacts.Add(new Mensajes(1, 1, 2, "Hola Como Estas", 12 / 28 / 1995));
            Contacts.Add(new Mensajes(1, 1, 2, "Hola Como Estas", 12 / 28 / 1995));
            Contacts.Add(new Mensajes(1, 1, 2, "Hola Como Estas", 12 / 28 / 1995));


            /*RestClient.GetMessageList(1, 2);*/

        }

        public Mensajes(int v1, int v2, int v3, string v4, int v5)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
            this.v5 = v5;
        }

        private Directorio _directorio { get; set; }

   
        private void NavigationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _directorio = new Directorio();
            _directorio.CargarArchivo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DatosDirCont.xml");
        }

        private void NavigationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _directorio.GuardarArchivo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DatosDirCont.xml");
        }
    }
}
