﻿using AutoMapper;
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
    [Route("api/Categoria")]
    public class CategoriaControllers : ControllerBase
    {
        private readonly ICategoriaRepositorio repositorio;
        private readonly IMapper mapper;

        public CategoriaControllers(ICategoriaRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]//Select
        public async Task<ActionResult<List<Categoria>>> Get() 
        {
            return await repositorio.Select();
        }

        [HttpPost]//Add
        public async Task<ActionResult<int>> Post(CrearCategoriaDTO entidadDTO)
        {
            try
            {
                Categoria entidad = mapper.Map<Categoria>(entidadDTO);

                return await repositorio.Insert(entidad);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]//Update
        public async Task<ActionResult> Put(int id, [FromBody] Categoria entidad) 
        {
            if (id != entidad.Id) 
            {
                return BadRequest("Los IDs no coinciden");
            }

            var ActualizaDato = await repositorio.SelectById(id);

            if (ActualizaDato == null) 
            {
                return BadRequest("No existe la categoria buscada");
            }

            ActualizaDato.Codigo = entidad.Codigo;
            ActualizaDato.Nombre = entidad.Nombre;

            try
            {
                await repositorio.Update(id, ActualizaDato);
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
            var ExisteVerif = await repositorio.Existe(id);
            if (!ExisteVerif) 
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
