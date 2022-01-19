using System;
using MediatR;
using FluentValidation;
using System.Threading.Tasks;
using System.Threading;
using TiendaServicios.API.Libro.Persistencia;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.API.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _contextoLibreria;
            private readonly IRabbitEventBus _eventBus;

            public Manejador(ContextoLibreria contextoLibreria, IRabbitEventBus eventBus)
            {
                _contextoLibreria = contextoLibreria;
                _eventBus = eventBus;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                _contextoLibreria.LibroMaterial.Add(new Modelo.LibroMaterial
                {
                    Titulo = request.Titulo,
                    AutorLibro = request.AutorLibro,
                    FechaPublicacion = request.FechaPublicacion
                });

                var r = await _contextoLibreria.SaveChangesAsync();
                _eventBus.Publish(new EmailEventoQueue("correo@dominio.com", request.Titulo, "Este es un ejemplo"));
                if (r > 0)
                    return Unit.Value;

                

                throw new Exception("Ocurrio un error agregando el Item");

            }
        }
    }
}
