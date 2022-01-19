using System;

namespace TiendaServicios.Api.Autor.Modelo
{
    public class GradoAcademico
    {
        public GradoAcademico()
        {
        }

        public int GradoAcademicoId { get; set; }
        public string Nombre { get; set; }
        public string CentroAcademico { get; set; }
        public DateTimeOffset? FechaGrado { get; set; }

        public int AutorLibroId { get; set; }
        public AutorLibro Autor { get; set; }
    }
}