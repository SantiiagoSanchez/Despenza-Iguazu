using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                return await context.Productos
                    .Include(x => x.Unidad)
                    .Include(p => p.Marca)
                    .Include(x => x.Categoria)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                // Opcional: loguear el error aquí
                throw new Exception("Error al obtener productos: " + e.Message, e);
            }
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
                throw ;
            }
        }

        public async Task<bool> UpdateEntidad(int id, Producto entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }

            var EntidadExiste = await SelectById(id);

            if (EntidadExiste == null)
            {
                return false;
            }


            try
            {
                context.Entry(EntidadExiste).CurrentValues.SetValues(entidad);
                //El metodo de arriba toma los valores de la entidad seleccionada por id (EntidadExistente)
                //y los actualiza con los de la entidad pasada como argumento (entidad).

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                //ImprimirError(ex);
                //Descomentar al Publicar el proyecto en IIS
                //Logger.LogError(ex);
                throw;
            }

        }

        public async Task<ActionResult<Producto>> GetIncludeId(int id)
        {
            Producto? prod = await context.Productos.Include(c => c.Marca).Include(a => a.Unidad).Include(t => t.Categoria).FirstOrDefaultAsync(x => x.Id == id);

            return prod;
        }
    }
}
