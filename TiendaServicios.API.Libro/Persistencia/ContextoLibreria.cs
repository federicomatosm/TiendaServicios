using System;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.Libro.Modelo;

namespace TiendaServicios.API.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria()
        {

        }
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options)
        {
        }

        public virtual DbSet<LibroMaterial> LibroMaterial { get; set; }
    }
}
