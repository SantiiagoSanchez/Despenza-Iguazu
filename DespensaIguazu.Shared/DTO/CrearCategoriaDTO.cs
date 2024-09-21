using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.Shared.DTO
{
    public class CrearCategoriaDTO
    {
        [Required(ErrorMessage = "El codigo de la categoria es obligatorio")]
        [MaxLength(100, ErrorMessage = "Maximo de numero de caracteres alcanzado {1}.")]
        public string Codigo { get; set; }


        //------------------------------------------------------------------------------------
        [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
        [MaxLength(100, ErrorMessage = "Maximo de numero de caracteres alcanzado {1}.")]
        public string Nombre { get; set; }
    }
}
