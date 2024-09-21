using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;

namespace DespensaIguazu.Server.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        public CategoriaRepositorio(Context context) : base(context)
        {
            
        }
    }
}
