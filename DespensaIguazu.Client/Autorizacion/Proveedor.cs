using Microsoft.AspNetCore.Components.Authorization;
using System.IO.Pipelines;
using System.Net;
using System.Security.Claims;

namespace DespensaIguazu.Client.Autorizacion
{
    public class Proveedor : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(4000);

            var NoAutorizado = new ClaimsIdentity();
            var UserAdmin = new ClaimsIdentity(
            new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Santiago"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("DNI", "46.033.394"),
                new Claim("Alias", "SantiDev")
            },
            authenticationType: "Ok"
            );


            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(UserAdmin)));
        }
    }
}
