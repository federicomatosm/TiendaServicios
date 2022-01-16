using System;
using System.Collections.Generic;


namespace TiendaServicios.Api.Autor.Modelo
{
    public class AutorLibro
    {
        public AutorLibro()
        {
        }

        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTimeOffset? FechaNacimiento { get; set; }

        public ICollection<GradoAcademico> ListaGradoAcademico { get; set; }

        public string AutorLibroGuid { get; set; }
        public string GradoAcademicoGuid { get; set; }
    }
}