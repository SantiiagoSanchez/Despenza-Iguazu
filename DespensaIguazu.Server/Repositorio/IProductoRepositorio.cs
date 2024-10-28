using DespensaIguazu.BD.Data.Entity;

namespace DespensaIguazu.Server.Repositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        Task<List<Producto>> SelectAll();
    }
}
