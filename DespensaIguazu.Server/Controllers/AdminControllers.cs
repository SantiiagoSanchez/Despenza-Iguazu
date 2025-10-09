using DespensaIguazu.BD.Data;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DespensaIguazu.Server.Controllers
{
    [ApiController]
    [Route("usuarios/Admin")]
    public class AdminControllers : ControllerBase
    {
        private readonly Context context;
        private readonly SignInManager<IdentityUser> userManager;

        public AdminControllers(Context context, SignInManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserListadoDTO>>> Get()
        {
            var user = context.Users.AsQueryable();
            var userDTO = user.Select(x => new UserListadoDTO
            { 
                Id = x.Id,
                Email = x.Email!
            }).ToList();

            return userDTO;
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RolDTO>>> GetRoles()
        {
            var roles = context.Roles.AsQueryable();
            var rolesDTO = roles.Select(x => new RolDTO
            {
                Nombre = x.Name!
            }).ToList();
            return rolesDTO;
        }

        [HttpPost("asignar-rol")]
        public async Task<ActionResult> AsignarRol(RolEditarDTO dto)
        {
            var usuario = await userManager.UserManager.FindByIdAsync(dto.UsuarioId);
            if (usuario == null) { return NotFound("Usuario no encontrado"); }
            await userManager.UserManager.AddToRoleAsync(usuario, dto.Rol);
            return NoContent();
        }

        [HttpPost("remover-rol")]
        public async Task<ActionResult> RemoverRol(RolEditarDTO dto)
        {
            var usuario = await userManager.UserManager.FindByIdAsync(dto.UsuarioId);
            if (usuario == null) { return NotFound("Usuario no encontrado"); }
            await userManager.UserManager.RemoveFromRoleAsync(usuario, dto.Rol);
            return NoContent();
        }
    }
}
