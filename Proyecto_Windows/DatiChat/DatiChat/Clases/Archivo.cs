using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatiChat.Clases
{
    public class Archivo
    {
        public int id { get; set; }
        public byte[] data { get; set; }
        public string name { get; set; }
        public string contentType { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public DateTime date { get; set; }

        public Archivo()
        {

        }

        public Archivo(int File_id, byte[] File_data, string File_name, string File_contentType, 
            int user_from, int user_to, DateTime File_date)
        {
            id = File_id;
            data = File_data;
            name = File_name;
            contentType = File_contentType;
            from = user_from;
            to = user_to;
            date = File_date;
        }


    }
}
