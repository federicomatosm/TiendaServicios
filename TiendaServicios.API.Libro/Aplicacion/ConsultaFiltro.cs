using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.Libro.Modelo;
using TiendaServicios.API.Libro.Persistencia;

namespace TiendaServicios.API.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibroMaterialDto>
        {
            public Guid? LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDto>
        {
            private readonly ContextoLibreria _contextoLibreria;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contextoLibreria, IMapper mapper)
            {
                _contextoLibreria = contextoLibreria;
                _mapper = mapper;
            }
            public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contextoLibreria.LibroMaterial.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();

                if (libro == null)
                    throw new Exception("No se encontro el libro");

                var retorno = _mapper.Map<LibroMaterial, LibroMaterialDto>(libro);

                return retorno;
            }
        }
    }
}
