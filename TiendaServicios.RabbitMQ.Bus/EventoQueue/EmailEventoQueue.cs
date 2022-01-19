using System;
using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.RabbitMQ.Bus.EventoQueue
{
	public class EmailEventoQueue : Evento
	{
		public EmailEventoQueue(string destinatario, string titulo, string contenido)
		{
			Destinatario = destinatario;
			Titulo = titulo;
			Contenido = contenido;
		}


        public string Destinatario { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }

    }
}

