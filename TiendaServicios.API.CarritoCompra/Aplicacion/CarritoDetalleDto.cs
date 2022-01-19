using System;
namespace TiendaServicios.API.CarritoCompra.Aplicacion
{
    public class CarritoDetalleDto
    {
        public Guid? LibroId { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get; set; }
        public DateTimeOffset? FechaPublicacion { get; set; }
    }
}
