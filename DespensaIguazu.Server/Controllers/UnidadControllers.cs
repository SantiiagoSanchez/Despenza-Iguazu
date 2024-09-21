using AutoMapper;
using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Server.Repositorio;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DespensaIguazu.Server.Controllers
{
    [ApiController]
    [Route("api/Unidad")]
    public class UnidadControllers : ControllerBase
    {
        private readonly IUnidadRepositorio repositorio;
        private readonly IMapper mapper;

        public UnidadControllers(IUnidadRepositorio repositorio, IMapper mapper) 
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]//Select
        public async Task<ActionResult<List<Unidad>>> Get() 
        {
            return await repositorio.Select();
        }

        [HttpPost]//Add
        public async Task<ActionResult<int>> Post(CrearUnidadDTO entidadDTO)
        {
            try
            {
                Unidad entidad = mapper.Map<Unidad>(entidadDTO);

                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]//Editar
        public async Task<ActionResult> Put(int id, [FromBody] Unidad entidad) 
        {
            if (id != entidad.Id) 
            {
                return BadRequest("Los IDs no coinciden");
            }

            var ActualizarDatos = await repositorio.SelectById(id);

            if (ActualizarDatos == null) 
            {
                return BadRequest("No existe la unidad buscada");
            }

            ActualizarDatos.Nombre = entidad.Nombre;

            try
            {
                await repositorio.Update(id, ActualizarDatos);
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
