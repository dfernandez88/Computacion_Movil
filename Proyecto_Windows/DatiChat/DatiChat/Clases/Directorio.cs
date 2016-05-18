using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DatiChat.Clases
{
    class Directorio
    {
        private List<Mensajes> _listaMensajes = null;

        /// <summary>
        /// Carga la Lista de contactos desde un archivo en disco
        /// </summary>
        public void CargarArchivo(string archivo)
        {
            if (File.Exists(archivo))
            {
                using (var sr = new StreamReader(archivo))
                {
                    var l = new XmlSerializer(typeof(List<Mensajes>));
                    _listaMensajes = (List<Mensajes>)l.Deserialize(sr);
                }
            }

        }

        /// <summary>
        /// Guarda la lista de contactos en un archivo en disco
        /// </summary>
        public void GuardarArchivo(string archivo)
        {
            using (var sw = new StreamWriter(archivo))
            {
                var g = new XmlSerializer(typeof(List<Mensajes>));
                g.Serialize(sw, _listaMensajes);
            }
        }
    }
}
