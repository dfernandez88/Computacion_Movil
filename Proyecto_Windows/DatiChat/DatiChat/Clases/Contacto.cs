using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatiChat.Clases
{
    public class Contacto
    {
       

        [JsonProperty("userId")]
        public int User_id { get; set; }

        [JsonProperty("userName")]
        public string User_name { get; set; }

        [JsonProperty("nombre")]
        public string Name { get; set; }


        public Contacto()
        {
            User_id = 0;
            User_name = "";
            Name = "";
        }

        public Contacto(int id, string user, string name)
        {
            User_id = id;
            User_name = user;
            Name = name;
        }

    }
}
