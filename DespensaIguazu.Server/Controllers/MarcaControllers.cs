using AutoMapper;
using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Server.Repositorio;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DespensaIguazu.Server.Controllers
{
    [ApiController]
    [Route("api/Marca")]
    public class MarcaControllers : ControllerBase
    {

        private readonly IMarcaRepositorio repositorio;
        private readonly IMapper mapper;

        public MarcaControllers(IMarcaRepositorio repositorio, IMapper mapper)
        {

            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]//Select
        public async Task<ActionResult<List<Marca>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearMarcaDTO entidadDTO)
        {
            try
            {
                Marca entidad = mapper.Map<Marca>(entidadDTO);

                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Marca entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("No coinciden los IDs");
            }

            var ActualizarDatos = await repositorio.SelectById(id);

            if (ActualizarDatos == null)
            {
                return BadRequest("No existe la marca buscada");
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var Verif = await repositorio.Existe(id);

            if (!Verif)
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
