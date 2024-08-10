using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.BD.Data.Entity
{
    public class Venta : EntityBase
    {
        public string CodigoVenta { get; set; }

        public DateTime Fecha { get; set; }

        public int Total { get; set; }
    }
}
