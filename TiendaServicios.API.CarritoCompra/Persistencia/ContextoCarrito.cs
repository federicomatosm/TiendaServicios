using System;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.CarritoCompra.Modelo;

namespace TiendaServicios.API.CarritoCompra.Persistencia
{
    public class ContextoCarrito : DbContext
    {
        public ContextoCarrito(DbContextOptions<ContextoCarrito> options):base(options)
        {
        }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}
