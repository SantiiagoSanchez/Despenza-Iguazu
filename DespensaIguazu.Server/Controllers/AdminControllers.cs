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
    }
}
