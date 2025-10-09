
using System.Text;
using System.Text.Json;

namespace DespensaIguazu.Client.Servicios
{
    public class HttpServicio : IHttpServicio
    {
        private readonly HttpClient http;

        public HttpServicio(HttpClient http)
        {
            this.http = http;
        }

        public async Task<HttpRespuesta<T>> Get<T>(string url)
        {
            var response = await http.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<T>(response);
                return new HttpRespuesta<T>(respuesta, false, response);
            }
            else
            {
                return new HttpRespuesta<T>(default, true, response);
            }
        }

        public async Task<HttpRespuesta<TResp?>> Post<T, TResp>(string url, T entidad) 
        {
            var EnviarJSON = JsonSerializer.Serialize(entidad);
            var EnviarCONTENIDO = new StringContent(EnviarJSON, Encoding.UTF8, "application/json");
            var Response = await http.PostAsync(url, EnviarCONTENIDO);
            if (Response.IsSuccessStatusCode)
            {
                var Respuesta = await DesSerializar<TResp>(Response);
                return new HttpRespuesta<TResp?>(Respuesta, false, Response);
            }
            else 
            {
                return new HttpRespuesta<TResp?>(default, true, Response);
            }
        }

        public async Task<HttpRespuesta<object>> Put<T>(string url, T entidad)
        {
            var EnviarJson = JsonSerializer.Serialize(entidad);

            var EnviarContenido = new StringContent(EnviarJson, Encoding.UTF8, "application/json");

            var Response = await http.PutAsync(url, EnviarContenido);

            if (Response.IsSuccessStatusCode) 
            {
                //var Respuesta = await DesSerializar<object>(Response);
                return new HttpRespuesta<object>(null, false, Response);
            }
            else 
            {
                return new HttpRespuesta<object>(default, true, Response);
            }
        }

        public async Task<HttpRespuesta<object>> Delete(string url)
        {
            var respuesta = await http.DeleteAsync(url);
            return new HttpRespuesta<object>(null,
                                             !respuesta.IsSuccessStatusCode,
                                             respuesta);
        }

        private async Task<T?> DesSerializar<T>(HttpResponseMessage response)
        {
            var RespuestaSTR = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(RespuestaSTR))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(RespuestaSTR, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
