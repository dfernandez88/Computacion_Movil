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
        public Mensajes()
        {
            InitializeComponent();

            List<Mensajes> Contacts = RestClient.GetMessageList(1, 2);
            
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
