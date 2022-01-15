using System;
using MediatR;
using FluentValidation;
using System.Threading.Tasks;
using System.Threading;
using TiendaServicios.API.Libro.Persistencia;



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

            public Manejador(ContextoLibreria contextoLibreria)
            {
                _contextoLibreria = contextoLibreria;
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

                if (r > 0)
                    return Unit.Value;

                throw new Exception("Ocurrio un error agregando el Item");

            }
        }
    }
}
