using AutoMapper;
using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Server.Repositorio;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DespensaIguazu.Server.Controllers
{
    [ApiController]
    [Route("api/Producto")]
    public class ProductoControllers : ControllerBase
    {
        
        private readonly IProductoRepositorio repositorio;
        private readonly IMapper mapper;

        public ProductoControllers(IProductoRepositorio repositorio, IMapper mapper)
        {
            
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet] // Select
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
        [HttpPost]//Add
        public async Task<ActionResult<int>> Post(CrearProductoDTO entidadDTO)
        {
            try
            {
                Producto entidad = mapper.Map<Producto>(entidadDTO);
                
                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]//Editar
        public async Task<ActionResult> Put(int id, EditarPrecioDTO entidad)
        {

            try
            {
                if (id != entidad.Id)
                {
                    return BadRequest("Datos Incorrectos");
                }
                var pepe = await repositorio.UpdateDTO(id, entidad);

                if (!pepe)
                {
                    return BadRequest("No se pudo actualizar el precio del producto");
                }
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
                return Ok();
            }
            else 
            {
                return BadRequest();
            }

        }
    }
}
