using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatiChat.Clases
{
    public class Mensaje
    {

        [JsonProperty("id")]
        [JsonIgnore]
        public int Message_id { get; set; }
        [JsonProperty("from")]
        public int User_from { get; set; }
        [JsonProperty("to")]
        public int User_to { get; set; }
        [JsonProperty("text")]
        public String Message { get; set; }
        [JsonProperty("date")]
        [JsonIgnore]
        public DateTime Date_message { get; set; }

        public Mensaje()
        {
            
        }
        public Mensaje(int id, int from, int to, String message1, DateTime time)
        {
            Message_id = id;
            User_from = from;
            User_to = to;
            Message = message1;
            Date_message = time;
        }
    }
}
