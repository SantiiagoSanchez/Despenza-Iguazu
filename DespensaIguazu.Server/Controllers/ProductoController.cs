using AutoMapper;
using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Server.Repositorio;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace DespensaIguazu.Server.Controllers
{
    [ApiController]
    [Route("api/Producto")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductoController : ControllerBase
    {

        private readonly IProductoRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly IOutputCacheStore outputCacheStore;
        private const string CacheKey = "ProductosCache";

        public ProductoController(IProductoRepositorio repositorio, IMapper mapper, IOutputCacheStore outputCacheStore)
        {

            this.repositorio = repositorio;
            this.mapper = mapper;
            this.outputCacheStore = outputCacheStore;
        }

        [HttpGet] // Select
        [OutputCache(Tags = [CacheKey])]

        public async Task<ActionResult<List<Producto>>> Get()
        {
            try
            {
                var productos = await repositorio.SelectAll();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return Problem("❌ Error al obtener productos: " + ex.Message);
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var producto = await repositorio.GetIncludeId(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]//Add
        public async Task<ActionResult<int>> Post(CrearProductoDTO entidadDTO)
        {
            try
            {
                Producto entidad = mapper.Map<Producto>(entidadDTO);
                await outputCacheStore.EvictByTagAsync(CacheKey, default);
                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]//Editar
        public async Task<ActionResult> Put(int id, Producto entidad)
        {

            try
            {
                if (id != entidad.Id)
                {
                    return BadRequest("Datos Incorrectos");
                }
                var pepe = await repositorio.UpdateEntidad(id, entidad);

                if (!pepe)
                {
                    return BadRequest("No se pudo actualizar el producto");
                }
                await outputCacheStore.EvictByTagAsync(CacheKey, default);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id:int}")]//Borrar
        public async Task<ActionResult> Delete(int id)
        {
            var Verificacion = await repositorio.Existe(id);

            if (!Verificacion)
            {
                return BadRequest($"El id {id} no existe");
            }

            if (await repositorio.Delete(id))
            {
                await outputCacheStore.EvictByTagAsync(CacheKey, default);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
