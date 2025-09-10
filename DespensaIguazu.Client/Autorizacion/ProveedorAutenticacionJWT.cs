using DespensaIguazu.Client.Servicios;
using DespensaIguazu.Shared.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace DespensaIguazu.Client.Autorizacion
{
    public class ProveedorAutenticacionJWT : AuthenticationStateProvider, ILoginService
    {
        public static readonly string TOKENKEY = "TOKENKEY";
        public static readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;

        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public ProveedorAutenticacionJWT(IJSRuntime js, HttpClient httpClient)
        {
            this.js = js;
            this.httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.ObtenerDeLocalStorage(TOKENKEY);
            if (token is null)
            {
                return Anonimo;
            }

            return ConstruirAuthenticationState(token.ToString()!);
        }

        private AuthenticationState ConstruirAuthenticationState(string token) //Esto en realidad no es el token sino que despues hay que poner el DTO
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var claims = ParsearClaimsDelJWT(token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        private IEnumerable<Claim> ParsearClaimsDelJWT(string token)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenDeserializado = jwtSecurityTokenHandler.ReadJwtToken(token);
            return tokenDeserializado.Claims;
        }

        public async Task Login(UserTokenDTO TokenDTO)
        {
            await js.GuardarEnLocalStorage(TOKENKEY, TokenDTO.Token);
            await js.GuardarEnLocalStorage(EXPIRATIONTOKENKEY, TokenDTO.Expiration.ToString());
            var authState = ConstruirAuthenticationState(TokenDTO.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await js.RemoverDeLocalStorage(TOKENKEY);
            await js.RemoverDeLocalStorage(EXPIRATIONTOKENKEY);
            httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));    
        }
    }
}
