using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Shared.DTO;

namespace DespensaIguazu.Server.Repositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        Task<List<Producto>> SelectAll();
        Task<bool> UpdateDTO(int id, EditarPrecioDTO entidad);
    }
}
