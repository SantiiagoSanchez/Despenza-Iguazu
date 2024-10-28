using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DespensaIguazu.Server.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly Context context;

        public ProductoRepositorio(Context context) : base (context)
        {
            this.context = context;
        }

        public async Task<List<Producto>> SelectAll()
        {
            return await context.Productos.Include(x => x.Unidad).Include(p => p.Marca).Include(x => x.Categoria).ToListAsync();
        }
    }
}
