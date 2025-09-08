using Microsoft.JSInterop;

namespace DespensaIguazu.Client.Servicios
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<bool> Confirmar(this IJSRuntime js, string mensaje)
        {
            return await js.InvokeAsync<bool>("confirm", mensaje);
        }

        public static async ValueTask<object> GuardarEnLocalStorage(this IJSRuntime js, string llave, string contenido) 
        {
            return await js.InvokeAsync<object>("localStorage.setItem", llave, contenido);
        }

        public static async ValueTask<string> ObtenerDeLocalStorage(this IJSRuntime js, string llave)
        {
            return await js.InvokeAsync<string>("localStorage.getItem", llave);
        }

        public static async ValueTask<object> RemoverDeLocalStorage(this IJSRuntime js, string llave)
        {
            return await js.InvokeAsync<object>("localStorage.removeItem", llave);
        }
    }
}
