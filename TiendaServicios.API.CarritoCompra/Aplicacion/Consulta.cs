using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TiendaServicios.API.CarritoCompra.InterfacesRemota;
using TiendaServicios.API.CarritoCompra.Persistencia;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace TiendaServicios.API.CarritoCompra.Aplicacion
{
    public class Consulta
    {

       public class Ejecuta : IRequest<CarritoDto>
        {
            public int CarritoSessionId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly ContextoCarrito _contextoCarrito;
            private readonly IServicioLibro _serviceLibro;

            public Manejador(ContextoCarrito contextoCarrito, IServicioLibro servicioLibro)
            {
                _contextoCarrito = contextoCarrito;
                _serviceLibro = servicioLibro;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSession = await _contextoCarrito.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSessionId);
                var carritoSessionDetalle = await _contextoCarrito.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSessionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDto>();

                foreach(var libro in carritoSessionDetalle)
                {
                  var response = await  _serviceLibro.GetLibro(new Guid(libro.ProductoSeleccionado));

                    if (response.resultado)
                    {
                        var objetoLibro = response.libro;
                        var carritoDetalle = new CarritoDetalleDto
                        {
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId

                        };

                        listaCarritoDto.Add(carritoDetalle);
                    }
                }

                var carritoDto = new CarritoDto
                {
                    CarritoId = carritoSession.CarritoSesionId,
                    FechaCreacionSesion = carritoSession.FechaCreacion,
                    ListaProducto = listaCarritoDto
                };

                return carritoDto;
            }
        }
    }
}
