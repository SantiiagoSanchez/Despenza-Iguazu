using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.Shared.DTO
{
    public class UserListadoDTO
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public int Telefono { get; set; }

        public string Rol { get; set; } = null!;
    }
}
