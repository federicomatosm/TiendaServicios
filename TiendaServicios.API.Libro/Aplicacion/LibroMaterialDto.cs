using System;
namespace TiendaServicios.API.Libro.Aplicacion
{
    public class LibroMaterialDto
    {
        public LibroMaterialDto()
        {
        }

        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTimeOffset? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }
    }
}
