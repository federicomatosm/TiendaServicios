using System;
using MediatR;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.Comandos;
using TiendaServicios.RabbitMQ.Bus.Eventos;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.DependencyInjection;

namespace TiendaServicios.RabbitMQ.Bus.Implement
{
    public class RabbitEventBus : IRabbitEventBus
	{
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _manejadores;
        private readonly List<Type> _eventoTipos;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitEventBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _manejadores = new();
            _eventoTipos = new();
            _serviceScopeFactory = serviceScopeFactory;

        }

        public Task EnviarComando<T>(T comando) where T : Comando
        {
            return _mediator.Send(comando);
        }

        public void Publish<T>(T evento) where T : Evento
        {
            var factory = new ConnectionFactory() { HostName = "rabbit-federico-web" };
            using(var conexion = factory.CreateConnection())
            using(var canal = conexion.CreateModel())
            {
                var nombreEv = evento.GetType().Name;
                canal.QueueDeclare(nombreEv, false, false, false, null);
                var mensaje = JsonSerializer.Serialize(evento);

                var body = Encoding.UTF8.GetBytes(mensaje);
                canal.BasicPublish("", nombreEv, null, body);


            }
        }

        public void Suscribe<T, TH>()
            where T : Evento
            where TH : IEventoManejador<T>
        {
            var eventoNombre = typeof(T).Name;
            var manejadorEventoTipo = typeof(TH);

            if (!_eventoTipos.Contains(typeof(T))) {
                _eventoTipos.Add(typeof(T));
            }
            if (!_manejadores.ContainsKey(eventoNombre))
            {
                _manejadores.Add(eventoNombre, new List<Type>());
            }

            if(_manejadores[eventoNombre].Any(x=> x.GetType()== manejadorEventoTipo))
            {
                throw new ArgumentException($"El manejador {manejadorEventoTipo.Name} fue registrado anteriormente por {eventoNombre}");
            }

            _manejadores[eventoNombre].Add(manejadorEventoTipo);

            var factory = new ConnectionFactory()
            {
                HostName = "rabbit-federico-web",
                DispatchConsumersAsync = true
            };

            var conexion = factory.CreateConnection();
            var canal = conexion.CreateModel();

            canal.QueueDeclare(eventoNombre, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(canal);
            consumer.Received += Consumer_Delegate;

            canal.BasicConsume(eventoNombre, true, consumer);
        }

        private async Task Consumer_Delegate(object sender, BasicDeliverEventArgs e)
        {
            var nombreEvento = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());
            try
            {
                if (_manejadores.ContainsKey(nombreEvento))
                {
                    using (var scope = _serviceScopeFactory.CreateAsyncScope())
                    {
                        var suscriptions = _manejadores[nombreEvento];
                        foreach (var sb in suscriptions)
                        {
                            var manejador = scope.ServiceProvider.GetService(sb); //.//Activator.CreateInstance(sb);
                            if (manejador == null) continue;

                            var tipoEvento = _eventoTipos.SingleOrDefault(x => x.Name == nombreEvento);
                            var eventoDs = JsonSerializer.Deserialize(message, tipoEvento);

                            var tipoConcreto = typeof(IEventoManejador<>).MakeGenericType(tipoEvento);


                            await (Task)tipoConcreto.GetMethod("Handle").Invoke(manejador, new object[] { eventoDs });

                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}

