using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.Shared.DTO
{
    public class UserInfoDTO
    {
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public int Telefono { get; set; }

        public string Direccion { get; set; } = null!;
    }
}
