using DespensaIguazu.BD.Data.Entity;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DespensaIguazu.Server.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<MiUsuario> userManager;
        private readonly SignInManager<MiUsuario> signInManager;
        private readonly IConfiguration configuration;

        public UsuarioController(UserManager<MiUsuario> userManager,
                                 SignInManager<MiUsuario> signInManager,
                                 IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UserTokenDTO>> CreateUser([FromBody] UserInfoDTO dto)
        {
            var usuario = new MiUsuario { UserName = dto.Email, Email = dto.Email, Nombre = dto.Nombre, Direccion = dto.Direccion, Telefono = dto.Telefono };

            var resultado = await userManager.CreateAsync(usuario, dto.Password);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(usuario);
            }
            else
            {
                return BadRequest(resultado.Errors.First());
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] LoginDTO dto)
        {
            var resultado = await signInManager.PasswordSignInAsync(dto.Email, dto.Password, isPersistent: false, lockoutOnFailure: false);
            if (!resultado.Succeeded)
            {
                return BadRequest("Login incorrecto");
                
            }

            var usuario = await userManager.FindByEmailAsync(dto.Email);
            if (usuario == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            return await ConstruirToken(usuario);
        }

        public async Task<ActionResult<UserTokenDTO>> ConstruirToken(MiUsuario userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Name, userInfo.Nombre),
                new Claim(ClaimTypes.StreetAddress, userInfo.Direccion)

            };

            var usuario = await userManager.FindByEmailAsync(userInfo.Email);

            var roles = await userManager.GetRolesAsync(usuario!);
            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]!));

            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMonths(1);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiracion,
                signingCredentials: credenciales
                );
            return new UserTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiracion
            };
        }
    }
}
