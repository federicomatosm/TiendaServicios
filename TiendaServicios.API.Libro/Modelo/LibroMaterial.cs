using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaServicios.API.Libro.Modelo
{
    public class LibroMaterial
    {

       
        [Key]
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTimeOffset? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }

    }
}
