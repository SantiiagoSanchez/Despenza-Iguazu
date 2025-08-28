using DespensaIguazu.BD.Migrations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.BD.Data.Entity
{
    public class DespensaUsuario : IdentityUser
    {
        public string Codigo { get; set; } = string.Empty; //Viendo como funciona el IdentityUser
    }
}
