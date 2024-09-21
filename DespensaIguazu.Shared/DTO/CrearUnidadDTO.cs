using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.Shared.DTO
{
    public class CrearUnidadDTO
    {
        [Required(ErrorMessage = "El nombre de la unidad es obligatorio")]
        [MaxLength(100, ErrorMessage = "Maximo de numero de caracteres alcanzado {1}.")]
        public string Nombre { get; set; }
    }
}
