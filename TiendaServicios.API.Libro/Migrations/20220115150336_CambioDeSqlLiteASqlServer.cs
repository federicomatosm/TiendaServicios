using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaServicios.API.Libro.Migrations
{
    public partial class CambioDeSqlLiteASqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LibroMaterial",
                columns: table => new
                {
                    LibreriaMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaPublicacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AutorLibro = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroMaterial", x => x.LibreriaMaterialId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibroMaterial");
        }
    }
}
