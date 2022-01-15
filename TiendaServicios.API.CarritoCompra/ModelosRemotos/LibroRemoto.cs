using System;
namespace TiendaServicios.API.CarritoCompra.ModelosRemotos
{
    public class LibroRemoto
    {
        public LibroRemoto()
        {
        }

        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }
    }
}
