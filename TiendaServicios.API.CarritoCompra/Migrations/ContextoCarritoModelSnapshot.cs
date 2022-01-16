﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TiendaServicios.API.CarritoCompra.Persistencia;

#nullable disable

namespace TiendaServicios.API.CarritoCompra.Migrations
{
    [DbContext(typeof(ContextoCarrito))]
    partial class ContextoCarritoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TiendaServicios.API.CarritoCompra.Modelo.CarritoSesion", b =>
                {
                    b.Property<int>("CarritoSesionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CarritoSesionId");

                    b.ToTable("CarritoSesion");
                });

            modelBuilder.Entity("TiendaServicios.API.CarritoCompra.Modelo.CarritoSesionDetalle", b =>
                {
                    b.Property<int>("CarritoSesionDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CarritoSesionId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProductoSeleccionado")
                        .HasColumnType("longtext");

                    b.HasKey("CarritoSesionDetalleId");

                    b.HasIndex("CarritoSesionId");

                    b.ToTable("CarritoSesionDetalle");
                });

            modelBuilder.Entity("TiendaServicios.API.CarritoCompra.Modelo.CarritoSesionDetalle", b =>
                {
                    b.HasOne("TiendaServicios.API.CarritoCompra.Modelo.CarritoSesion", "carritoSesion")
                        .WithMany("ListaDetalle")
                        .HasForeignKey("CarritoSesionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("carritoSesion");
                });

            modelBuilder.Entity("TiendaServicios.API.CarritoCompra.Modelo.CarritoSesion", b =>
                {
                    b.Navigation("ListaDetalle");
                });
#pragma warning restore 612, 618
        }
    }
}
