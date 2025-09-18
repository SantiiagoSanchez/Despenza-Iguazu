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
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public UsuarioController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UserTokenDTO>> CreateUser([FromBody] UserInfoDTO dto)
        {
            var usuario = new IdentityUser { UserName = dto.Email, Email = dto.Email };

            var resultado = await userManager.CreateAsync(usuario, dto.Password);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(dto);
            }
            else
            {
                return BadRequest(resultado.Errors.First());
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] UserInfoDTO dto)
        {
            var resultado = await signInManager.PasswordSignInAsync(dto.Email, dto.Password, isPersistent: false, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return await ConstruirToken(dto);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        public async Task<ActionResult<UserTokenDTO>> ConstruirToken(UserInfoDTO userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Name, userInfo.Email)

            };
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
