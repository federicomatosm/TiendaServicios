using System;
namespace TiendaServicios.API.CarritoCompra.Modelo
{
    public class CarritoSesionDetalle
    {
       

        public int CarritoSesionDetalleId { get; set; }
        public DateTimeOffset? FechaCreacion { get; set; }
        public string ProductoSeleccionado { get; set; }

        public int CarritoSesionId { get; set; }

        public CarritoSesion carritoSesion { get; set; }
    }
}
