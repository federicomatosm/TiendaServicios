using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaServicios.API.Libro.Modelo
{
    public class LibroMaterial
    {
        public LibroMaterial()
        {
        }

        [Key]
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }

    }
}
