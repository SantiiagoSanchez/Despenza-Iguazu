using DespensaIguazu.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.Shared.DTO
{
    public class CrearProductoDTO
    {
        [Required(ErrorMessage = "El codigo del producto es obligatorio")]
        [MaxLength(100, ErrorMessage = "Maximo de numero de caracteres alcanzado {1}.")]
        public string Codigo { get; set; }

        //---------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [MaxLength(100, ErrorMessage = "Maximo de numero de caracteres alcanzado {1}.")]
        public string Nombre { get; set; }

        //---------------------------------------------------------------------------------------

        [Required(ErrorMessage = "La marca del producto es obligatorio")]

        public int MarcaId { get; set; }

        public Marca? Marca;
        //---------------------------------------------------------------------------------------

        [Required(ErrorMessage = "La marca del producto es obligatorio")]

        public int UnidadId { get; set; }

        public Unidad? Unidad;
        //---------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        public decimal Precio { get; set; }

        //---------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El Stock del producto es obligatorio")]
        public int Stock { get; set; }

        //---------------------------------------------------------------------------------------

        [Required(ErrorMessage = "La categoria es obligatoria")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria;
    }
}
