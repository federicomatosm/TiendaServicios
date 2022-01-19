using System;
namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class AutorDto
    {
        public AutorDto()
        {
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTimeOffset? FechaNacimiento { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}
