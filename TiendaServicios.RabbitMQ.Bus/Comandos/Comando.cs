using System;
using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.RabbitMQ.Bus.Comandos
{
    public abstract class Comando : Message
	{
        public DateTimeOffset Timestamp { get; protected set; }

        protected Comando()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}

