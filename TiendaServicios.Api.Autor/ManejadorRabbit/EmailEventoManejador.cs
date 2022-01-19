using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
	{
        private readonly ILogger _logger;

        public EmailEventoManejador()
        {

        }
        public EmailEventoManejador(ILogger<EmailEventoManejador> logger)
		{
            _logger = logger;
        }

        public Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation($"Este es el valor de consumo desde rabbitmq {@event.Titulo}");

            return Task.CompletedTask;
        }
    }
}

