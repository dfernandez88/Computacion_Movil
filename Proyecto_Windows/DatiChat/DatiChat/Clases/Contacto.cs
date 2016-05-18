using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatiChat.Clases
{
    public class Contacto
    {
        private int _user_id;

        public int User_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        private string _user_name;

        public string User_name
        {
            get { return _user_name; }
            set { _user_name = value; } 
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Contacto()
        {
            _user_id = 0;
            _user_name = "";
            _name = "";
        }

        public Contacto(int id, string user, string name)
        {
            _user_id = id;
            _user_name = user;
            _name = name;
        }

    }
}
