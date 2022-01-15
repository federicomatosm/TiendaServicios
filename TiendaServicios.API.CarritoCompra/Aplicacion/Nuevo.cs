using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TiendaServicios.API.CarritoCompra.Modelo;
using TiendaServicios.API.CarritoCompra.Persistencia;

namespace TiendaServicios.API.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
       public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoCarrito _contextoCarrito;

            public Manejador(ContextoCarrito contextoCarrito)
            {
                _contextoCarrito = contextoCarrito;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion { FechaCreacion = request.FechaCreacionSesion};

                _contextoCarrito.CarritoSesion.Add(carritoSesion);
                var r = await _contextoCarrito.SaveChangesAsync();

                if (r == 0)
                    throw new Exception("Ocurrio un error guardando el item en el carrito de compras");

                var idSesion = carritoSesion.CarritoSesionId;

                foreach(var p in request.ProductoLista)
                {
                    _contextoCarrito.CarritoSesionDetalle.Add(new CarritoSesionDetalle {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = idSesion,
                        ProductoSeleccionado = p
                    });
                }

                r = await _contextoCarrito.SaveChangesAsync();

                if (r > 0)
                    return Unit.Value;

                throw new Exception("Ocurrio un error guardando el detalle del carrito");

            }

           
        }
    }
}
