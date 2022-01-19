using System;
namespace TiendaServicios.RabbitMQ.Bus.Eventos
{
	public class Evento
	{
        public DateTimeOffset Timespam { get; protected set; }

        protected Evento()
        {
            Timespam = DateTimeOffset.UtcNow;
        }
    }
}

