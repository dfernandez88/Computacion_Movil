using DatiChat.Clases;
using DatiChat.Rest;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para Archivos.xaml
    /// </summary>
    public partial class Archivos : Window
    {
        private OpenFileDialog file { get; set; }
        private Contacto user_from { get; set; }
        private Contacto user_to { get; set; }
        private List<Archivo> Files { get; set; }

        public Archivos(Contacto from, Contacto to)
        {
            InitializeComponent();
            this.user_from = from;
            this.user_to = to;

            try
            {
                //Cambiar el id (obtenerlo del id)
                Files = RestClient.GetFile(user_from.User_id, user_to.User_id);
                Archivos_usuarios.ItemsSource = Files;

                ///////////////////////
                for (int i = 0; i < Files.Count; i++)
                {
                    Archivo value = Files[i];
                    value.data = RestClient.GetFilebyId(value.id);
                    string dir = "C:/Users/DaRiLaptop/Desktop/Proyecto_Windows/Descargas_DatiChat" + value.name;

                    BinaryWriter Writer = null;
                    Writer = new BinaryWriter(File.OpenWrite(dir));

                    Writer.Write(value.data);
                    Writer.Flush();
                    Writer.Close();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //FIXME
            }
        }

        private void Seleccionar_Click(object sender, RoutedEventArgs e)
        {
            List<string> FilePath = new List<string>();
            this.file = new OpenFileDialog();
            file.Multiselect = true;

            String[] nombresArchivos = null;
            if (file.ShowDialog() == true)
            {
                nombresArchivos = file.SafeFileNames;
                FilePath.AddRange(file.FileNames);
                textBox.Text = file.SafeFileName;
                RestClient.PostFile(user_from.User_id, user_to.User_id, file);                
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Mensajes msn = new Mensajes(user_from, user_to);
            msn.Owner = this;
            msn.Show();
            this.Hide();
        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            Files = RestClient.GetFile(user_from.User_id, user_to.User_id);
            Archivos_usuarios.ItemsSource = Files;
            for (int i = 0; i < Files.Count; i++)
            {
                Archivo value = Files[i];
                value.data = RestClient.GetFilebyId(value.id);
                string dir = "C:/Users/DaRiLaptop/Documents/GitHub/Computacion_Movil/Proyecto_Windows/Descargas_DatiChat";

                BinaryWriter Writer = null;
                Writer = new BinaryWriter(File.OpenWrite(dir));

                Writer.Write(value.data);
                Writer.Flush();
                Writer.Close();
            }
        }

        private void Archivos_usuarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog descargas = new OpenFileDialog();
            descargas.InitialDirectory = "C:/Users/DaRiLaptop/Documents/GitHub/Computacion_Movil/Proyecto_Windows/Descargas_DatiChat";
        }
    }
}
