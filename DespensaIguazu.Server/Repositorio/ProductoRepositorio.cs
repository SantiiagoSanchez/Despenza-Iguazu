using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;

namespace DespensaIguazu.Server.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        public ProductoRepositorio(Context context) : base (context)
        {
            
        }
    }
}
