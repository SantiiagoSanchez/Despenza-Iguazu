
namespace DespensaIguazu.Client.Servicios
{
    public interface IHttpServicio
    {
        Task<HttpRespuesta<T>> Get<T>(string url);
        Task<HttpRespuesta<object>> Put<T>(string url, T entidad);
    }
}