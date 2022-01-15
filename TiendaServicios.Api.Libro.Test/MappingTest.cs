using System;
using AutoMapper;
using TiendaServicios.API.Libro.Aplicacion;
using TiendaServicios.API.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Test
{
	public class MappingTest: Profile
	{
		public MappingTest()
		{
			CreateMap<LibroMaterial, LibroMaterialDto>();
		}
	} 
}

