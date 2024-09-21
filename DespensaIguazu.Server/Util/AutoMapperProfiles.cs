using AutoMapper;
using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Shared.DTO;

namespace DespensaIguazu.Server.Util
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearCategoriaDTO, Categoria>();
            CreateMap<CrearMarcaDTO, Marca>();
            CreateMap<CrearUnidadDTO, Unidad>();
            CreateMap<CrearProductoDTO, Producto>();
        }
    }
}
