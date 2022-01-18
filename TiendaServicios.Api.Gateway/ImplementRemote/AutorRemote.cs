using System;
using System.Text.Json;
using TiendaServicios.Api.Gateway.RemoteInterface;
using TiendaServicios.Api.Gateway.RemoteModel;

namespace TiendaServicios.Api.Gateway.ImplementRemote
{
	public class AutorRemote : IAutorRemote
	{
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<AutorRemote> _logger;

        public AutorRemote(IHttpClientFactory httpClient, ILogger<AutorRemote> logger)
		{
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool resultado, AutorModeloRemote autor, string ErrorMessage)> GetAutor(Guid AutorId)
        {
            try
            {
                var cliente = _httpClient.CreateClient("AutorService");
                var respuesta = await cliente.GetAsync($"/Autor/{AutorId}");
                if (respuesta.IsSuccessStatusCode)
                {
                    var contenido = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<AutorModeloRemote>(contenido, opciones);

                    return (true, resultado, null);
                }

                return (false, null, respuesta.ReasonPhrase);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

           
        }
    }
}

