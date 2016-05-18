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
    }
}
