using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;

namespace DespensaIguazu.Server.Repositorio
{
    public class UnidadRepositorio : Repositorio<Unidad> , IUnidadRepositorio
    {
        public UnidadRepositorio(Context context) : base(context)
        {
            
        }
    }
}
