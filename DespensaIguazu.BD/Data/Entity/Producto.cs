using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.BD.Data.Entity
{

    [Index(nameof(Codigo), nameof(CategoriaId), Name = "Producto_UQ", IsUnique = true)]
    public class Producto : EntityBase
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

        public Marca Marca { get; set; }

        //---------------------------------------------------------------------------------------

        [Required(ErrorMessage = "La marca del producto es obligatorio")]

        public int UnidadId { get; set; }

        public Unidad Unidad { get; set; }

        //---------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        public decimal Precio { get; set; }

        //---------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El Stock del producto es obligatorio")]
        public int Stock { get; set; }

        //---------------------------------------------------------------------------------------

        [Required(ErrorMessage = "La categoria es obligatoria")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
    }
}
