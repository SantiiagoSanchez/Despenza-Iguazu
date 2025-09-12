using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DespensaIguazu.Server.Repositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        Task<Producto?> GetIncludeId(int id);
        Task<List<Producto>> SelectAll();
        Task<bool> UpdateDTO(int id, EditarPrecioDTO entidad);
        Task<bool> UpdateEntidad(int id, Producto entidad);
    }
}
