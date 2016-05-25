using DatiChat.Clases;
using DatiChat.Rest;
using Microsoft.Win32;
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
        private Contacto _user_to { get; set; }
        private Contacto _user_from { get; set; }

        public Mensajes(Contacto user_from, Contacto user_to)
        {
            InitializeComponent();
            this._user_from = user_from;
            this._user_to = user_to;

            List<Mensaje> Message = RestClient.GetMessageList(user_from.User_id, user_to.User_id);
            List<Mensaje> Message2 = RestClient.GetMessageList(user_to.User_id, user_from.User_id);
            Message = Message.Union(Message2).ToList();

            //Message = Message.OrderBy(p => p.Date_message).ToList();

            Message.Sort((x, y) => DateTime.Compare(x.Date_message , y.Date_message));

            mss.ItemsSource = Message;
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Contactos msn = new Contactos();
            msn.Owner = this;
            msn.Show();
            this.Hide();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            Archivos msn = new Archivos(_user_from,_user_to);
            msn.Owner = this;
            msn.Show();
            this.Hide();
            
            
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string msm = textBox.Text;
            if (RestClient.PostServiceMessages(_user_from.User_id, _user_to.User_id, msm)) {
                List<Mensaje> Message = RestClient.GetMessageList(_user_from.User_id, _user_to.User_id);
                List<Mensaje> Message2 = RestClient.GetMessageList(_user_to.User_id, _user_from.User_id);
                Message = Message.Union(Message2).ToList();
                //Message = Message.OrderBy(p => p.Date_message).ToList();
                Message.Sort((x, y) => DateTime.Compare(x.Date_message, y.Date_message));
                mss.ItemsSource = Message;
                textBox.Clear();
            }
            else
            {
                MessageBox.Show("No");
            }
            
        
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            List<Mensaje> Message = RestClient.GetMessageList(_user_from.User_id, _user_to.User_id);
            List<Mensaje> Message2 = RestClient.GetMessageList(_user_to.User_id, _user_from.User_id);
            Message = Message.Union(Message2).ToList();
            //Message = Message.OrderBy(p => p.Date_message).ToList();
            Message.Sort((x, y) => DateTime.Compare(x.Date_message, y.Date_message));
            mss.ItemsSource = Message;
        }
    }
}
