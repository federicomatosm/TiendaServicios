using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TiendaServicios.API.CarritoCompra.InterfacesRemota;
using TiendaServicios.API.CarritoCompra.ModelosRemotos;
using System.Text.Json;

namespace TiendaServicios.API.CarritoCompra.ServiciosRemoto
{
    public class ServicioLibro : IServicioLibro
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<ServicioLibro> _logger;

        public ServicioLibro(IHttpClientFactory httpClient, ILogger<ServicioLibro> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool resultado, LibroRemoto libro, string ErrorMessage)> GetLibro(Guid libroId)
        {
            try
            {

                var cliente = _httpClient.CreateClient("Libros");
                var responseMessage = await cliente.GetAsync($"/api/LibroMaterial/{ libroId}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var content = await responseMessage.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<LibroRemoto>(content, options);

                    return (true, resultado, string.Empty);
                }

                return (false, null, responseMessage.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

      
    }
}
