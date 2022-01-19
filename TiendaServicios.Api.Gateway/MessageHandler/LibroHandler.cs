using System;
using System.Diagnostics;
using System.Text.Json;
using TiendaServicios.Api.Gateway.RemoteInterface;
using TiendaServicios.Api.Gateway.RemoteModel;

namespace TiendaServicios.Api.Gateway.MessageHandler
{
	public class LibroHandler: DelegatingHandler
	{
        private readonly ILogger<LibroHandler> _logger;
        private readonly IAutorRemote _autorRemote;

        public LibroHandler(ILogger<LibroHandler> logger, IAutorRemote autorRemote)
        {
            _logger = logger;
            _autorRemote = autorRemote;
        }

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,  CancellationToken cancellationToken)
        {
            var tiempo = Stopwatch.StartNew();
            _logger.LogInformation("Inicia el request");
            var response = await base.SendAsync(request, cancellationToken);

            _logger.LogInformation($"Este proceso se hizo en {tiempo.ElapsedMilliseconds}ms");

            if (response.IsSuccessStatusCode)
            {
                var contentenido = await response.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<LibroModeloRemoto>(contentenido, opciones);
                var resultadoAutor = await _autorRemote.GetAutor(resultado.AutorLibro ?? Guid.Empty);
                if (resultadoAutor.resultado)
                {
                    var autor = resultadoAutor.autor;
                    resultado.AutoData = autor;

                    var resultadoStr = JsonSerializer.Serialize(resultado);
                    response.Content = new StringContent(resultadoStr, System.Text.Encoding.UTF8, "application/json"); 
                }
            }

            return response;
        }
	}
}

