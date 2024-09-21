using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;

namespace DespensaIguazu.Server.Repositorio
{
    public class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio
    {
        public MarcaRepositorio(Context context) : base(context)
        {
            
        }
    }
}
