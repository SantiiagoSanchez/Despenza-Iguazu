using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.BD.Data.Entity
{
    public class MiUsuario : IdentityUser
    {
        public string Nombre { get; set; }

        public int Telefono { get; set; }

        public string Direccion { get; set; }
    }
}
