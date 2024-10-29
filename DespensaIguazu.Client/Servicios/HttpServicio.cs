﻿
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

        private async Task<T?> DesSerializar<T>(HttpResponseMessage response)
        {
            var RespuestaSTR = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(RespuestaSTR, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}