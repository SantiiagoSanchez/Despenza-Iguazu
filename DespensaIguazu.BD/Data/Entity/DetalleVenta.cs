using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.BD.Data.Entity
{
    [Index(nameof(VentaId), nameof(ProductoId), Name = "DetalleVenta_UQ", IsUnique = true)]
    public class DetalleVenta : EntityBase
    {

        [Required(ErrorMessage = "El id de la Venta es obligatorio")]
        public int VentaId { get; set; }

        public Venta Venta { get; set; }

        //--------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El id del Producto es obligatorio")]
        public int ProductoId { get; set; }

        public Producto Producto { get; set; }


        public int Cantidad { get; set; }

        public int Precio { get; set; }
    }
}
