using AutoMapper;
using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DespensaIguazu.Server.Repositorio
{
    public class Repositorio<E> : IRepositorio<E> where E : class, IEntityBase
    {
        private readonly Context context;

        public Repositorio(Context context)
        {
            this.context = context;
        }


        public async Task<bool> Existe(int id)
        {
            var existe = await context.Set<E>().AnyAsync(x => x.Id == id);

            return existe;
        }

        public async Task<List<E>> Select()
        {
            return await context.Set<E>().ToListAsync();
        }

        public async Task<int> Insert(E entidad)
        {
            try
            {
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<E> SelectById(int id)
        {
            try
            {
                if (!await Existe(id))
                {
                    Console.WriteLine($"El producto con id {id} No existe");
                    return null;
                }

                return await context.Set<E>().FirstOrDefaultAsync(e => e.Id == id);


            }
            catch (Exception ex)
            {
                //ImprimirError(ex);
                //Descomentar al Publicar el proyecto en IIS
                //Logger.LogError(ex);
                throw;
            }
        }

        public async Task<bool> Update(int id, E entidad)
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


            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var Pepe = await SelectById(id);
            if (Pepe == null)
            {
                return false;
            }


            context.Set<E>().Remove(Pepe);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
