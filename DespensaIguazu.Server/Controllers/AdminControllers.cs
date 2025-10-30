using DespensaIguazu.BD.Data;
using DespensaIguazu.BD.Data.Entity;
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
        private readonly SignInManager<MiUsuario> signInManager;
        private readonly UserManager<MiUsuario> userManager;

        public AdminControllers(Context context, SignInManager<MiUsuario> signInManager, UserManager<MiUsuario> userManager)
        {
            this.context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserListadoDTO>>> Get()
        {
            var usuarios = userManager.Users.ToList();
            var lista = new List<UserListadoDTO>();

            foreach (var usuario in usuarios)
            {
                var roles = await userManager.GetRolesAsync(usuario); // trae la lista de roles
                lista.Add(new UserListadoDTO
                {
                    Id = usuario.Id,
                    Email = usuario.Email!,
                    Nombre = usuario.Nombre,
                    Telefono = usuario.Telefono,
                    Rol = roles.FirstOrDefault() ?? "Sin rol" // tomamos el primer rol
                });
            }

            return lista;
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
            var usuario = await signInManager.UserManager.FindByIdAsync(dto.UsuarioId);
            if (usuario == null) { return NotFound("Usuario no encontrado"); }
            await signInManager.UserManager.AddToRoleAsync(usuario, dto.Rol);
            return NoContent();
        }

        [HttpPost("remover-rol")]
        public async Task<ActionResult> RemoverRol(RolEditarDTO dto)
        {
            var usuario = await signInManager.UserManager.FindByIdAsync(dto.UsuarioId);
            if (usuario == null) { return NotFound("Usuario no encontrado"); }
            await signInManager.UserManager.RemoveFromRoleAsync(usuario, dto.Rol);
            return NoContent();
        }
    }
}
