using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DespensaIguazu.Server.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly Context context;

        public ProductoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Producto>> SelectAll()
        {
            return await context.Productos.Include(x => x.Unidad).Include(p => p.Marca).Include(x => x.Categoria).ToListAsync();
        }

        public async Task<bool> UpdateDTO(int id, EditarPrecioDTO entidad) 
        {
            if (id != entidad.Id)
            {
                return false;
            }

            var ActualizaDato = await SelectById(id);

            if (ActualizaDato == null)
            {
                return false;
            }

            ActualizaDato.Precio = entidad.Precio;

            try
            {
                //context.Set<Producto>().Update(ActualizaDato);
                context.Entry(ActualizaDato).Property(p => p.Precio).IsModified = true;
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
