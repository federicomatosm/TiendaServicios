using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TiendaServicios.Mensajeria.Email.Interface;
using TiendaServicios.Mensajeria.Email.Modelo;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
	{
        private readonly ILogger _logger;
        private readonly ISendGridEnviar _sendGridEnviar;
        private readonly IConfiguration _configuration;

        public EmailEventoManejador()
        {
           
        }
        public EmailEventoManejador(ILogger<EmailEventoManejador> logger, ISendGridEnviar sendGridEnviar, Microsoft.Extensions.Configuration.IConfiguration configuration)
		{
            _logger = logger;
            _sendGridEnviar = sendGridEnviar;
            _configuration = configuration;
        }

        public async Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation($"Este es el valor de consumo desde rabbitmq {@event.Titulo}");
            var objData = new SendGridData();
            objData.Contenido = @event.Contenido;
            objData.EmailDestinatario = @event.Destinatario;
            objData.NombreDestinatario = @event.Destinatario;
            objData.Titulo = @event.Titulo;
            objData.SendGridAPIKey = _configuration["SendGrid:ApiKey"];


            var resultado = await _sendGridEnviar.EnviarEmail(objData);

            if (resultado.resultado)
            {
                await Task.CompletedTask;
                return;
            }

          
        }
    }
}

