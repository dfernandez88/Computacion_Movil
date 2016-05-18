
using MvcApplication.Data;
using PortableClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication.Controllers
{
    public class ContactosController : ApiController
    {
        /* /api/contacts */
        public IEnumerable<Contacto> GetAllContacts()
        {
            return ContactosInformacion.getContactos();
        }

        /* /api/contacts/{id} */
        public IHttpActionResult GetContacto(int id)
        {
            var contacto = ContactosInformacion.getContactos().FirstOrDefault((p) => p.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }
            return Ok(contacto);
        }
    }
}
