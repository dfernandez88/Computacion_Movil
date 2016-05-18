using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatiChat.Clases
{
    class Mensajes
    {
        private int _message_id;
        public int Message_id
        {
            get { return _message_id; }
            set { _message_id = value; }
        }

        public int User_from { get; set; }
        public int User_to { get; set; }
        public String Message { get; set; }
        public DateTime Date_message { get; set; }

        public Mensajes(int id, int from, int to, String message1, DateTime time)
        {
            Message_id = id;
            User_from = from;
            User_to = to;
            Message = message1;
            Date_message = time;
        }
    }
}
