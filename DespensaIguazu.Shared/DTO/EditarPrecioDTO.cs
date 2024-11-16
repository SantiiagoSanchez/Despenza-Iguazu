using DespensaIguazu.BD.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.Shared.DTO
{
    public class EditarPrecioDTO
    {
        [Required(ErrorMessage = "El ID del producto es obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        public decimal Precio { get; set; }
    }
}
