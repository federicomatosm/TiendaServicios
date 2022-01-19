using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaServicios.API.CarritoCompra.Migrations
{
    public partial class ChangeDatesAndProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductoSeleccionado",
                table: "CarritoSesionDetalle",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "FechaCreacion",
                table: "CarritoSesionDetalle",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "FechaCreacion",
                table: "CarritoSesion",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductoSeleccionado",
                table: "CarritoSesionDetalle",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "CarritoSesionDetalle",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "CarritoSesion",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
