using System;
using AutoMapper;
using TiendaServicios.API.Libro.Modelo;

namespace TiendaServicios.API.Libro.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibroMaterial, LibroMaterialDto>();
        }
    }
}
