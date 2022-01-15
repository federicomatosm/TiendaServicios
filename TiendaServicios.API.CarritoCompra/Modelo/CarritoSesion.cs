using System;
using System.Collections.Generic;

namespace TiendaServicios.API.CarritoCompra.Modelo
{
    public class CarritoSesion
    {
        public CarritoSesion()
        {
        }

        public int CarritoSesionId { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public ICollection<CarritoSesionDetalle> ListaDetalle { get; set; }
    }
}
