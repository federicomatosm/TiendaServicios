using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.Libro.Modelo;
using TiendaServicios.API.Libro.Persistencia;

namespace TiendaServicios.API.Libro.Aplicacion
{
    public class Consulta
    {
       public class Ejecuta : IRequest<List<LibroMaterialDto>> { }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDto>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;


            public Manejador(ContextoLibreria contextoLibreria, IMapper mapper)
            {
                _contexto = contextoLibreria;
                _mapper = mapper;
            }


            public async Task<List<LibroMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibroMaterial.ToListAsync();
                return _mapper.Map<List<LibroMaterial>, List<LibroMaterialDto>>(libros);


            }
        }
    }
}
