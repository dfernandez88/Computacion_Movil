
using PortableClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Data
{
    public class ContactosInformacion
    {
        private static List<Contacto> Contactos;

        public static List<Contacto> getContactos()
        {
            if (Contactos == null)
            {
                Contactos = new List<Contacto>(5);
                Contactos.Add(new Contacto() { Id = 1, Nombre = "Juan Camilo", Apellido = "Rada", Telefono = "3256984" });
                Contactos.Add(new Contacto() { Id = 2, Nombre = "Fernando", Apellido = "Perez", Telefono = "3156925" });
                Contactos.Add(new Contacto() { Id = 3, Nombre = "Patricia", Apellido = "Gomez", Telefono = "3304569" });
                Contactos.Add(new Contacto() { Id = 4, Nombre = "Diana", Apellido = "Mesa", Telefono = "3254585" });
                Contactos.Add(new Contacto() { Id = 5, Nombre = "Catalina", Apellido = "Ordoñez", Telefono = "3161889" });
            }

            return Contactos;
        }

        public static long saveContacto(Contacto contacto) 
        {
            long maxId = Contactos.Max(x => x.Id);
            contacto.Id = maxId + 1;
            Contactos.Add(contacto);
            return contacto.Id;
        }
    }
}