using DespensaIguazu.Shared.DTO;

namespace DespensaIguazu.Client.Autorizacion
{
    public interface ILoginService
    {
        Task Login(UserTokenDTO TokenDTO);
        Task Logout();


    }
}
