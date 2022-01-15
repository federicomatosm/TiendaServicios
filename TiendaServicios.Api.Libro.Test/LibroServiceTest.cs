using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using TiendaServicios.API.Libro.Aplicacion;
using TiendaServicios.API.Libro.Modelo;
using TiendaServicios.API.Libro.Persistencia;
using Xunit;
namespace TiendaServicios.Api.Libro.Test
{

	public class LibroServiceTest
	{
		private IEnumerable<LibroMaterial> ObtenerDataPrueba()
        {
			A.Configure<LibroMaterial>()
				.Fill(x => x.Titulo).AsArticleTitle()
				.Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

			var listaData = A.ListOf<LibroMaterial>(30);
			listaData[0].LibreriaMaterialId = Guid.Empty; //esto para cuando vaya a hacer el metodo de test de buscar un libro por id

			return listaData;
        }

		private Mock<ContextoLibreria> CrearContexto()
        {
			var dataPrueba = ObtenerDataPrueba().AsQueryable();
			var dbSet = new Mock<DbSet<LibroMaterial>>();
			//como esta mock agrego las propiedades necesarias por ef 
			dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
			dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
			dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
			dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

			dbSet.As<IAsyncEnumerable<LibroMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
				.Returns(new AsyncEnumerator<LibroMaterial>(dataPrueba.GetEnumerator()));

			dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibroMaterial>(dataPrueba.Provider));

			var contexto = new Mock<ContextoLibreria>();
			contexto.Setup(x => x.LibroMaterial).Returns(dbSet.Object);

			return contexto;

			

		}

		[Fact]
		public async void GetLibros()
        {
			//que metodo del microsservicio libro hace la consulta de libros
			//1. Necesito emular la instancia de ef Contexto libreria
			//utilizar la libreria Moq para emular el contexto
			//2. Emular al IMapper, utilizar Moq para emularlo
			//3. instanciar la clase Manejador

			//only for debug
			//System.Diagnostics.Debugger.Launch();

			var mockContexto = CrearContexto();
			var mapperConfig = new MapperConfiguration(cfg => {
				cfg.AddProfile(new MappingTest());
					});

			var mockMapper = mapperConfig.CreateMapper();

			Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mockMapper);
			Consulta.Ejecuta request = new Consulta.Ejecuta();

			var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

			Assert.True(lista.Any());
        }

		[Fact]
		public async void GetLibroxId()
        {
			var mockContexto = CrearContexto();
			var mapConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MappingTest());
			});

			var mapper = mapConfig.CreateMapper();
			var request = new ConsultaFiltro.LibroUnico();
			request.LibroId = Guid.Empty;

			var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);
			var libro =await manejador.Handle(request, new System.Threading.CancellationToken());

			Assert.NotNull(libro);
			Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

		[Fact]
		public async void GuardarLibro()
        {
			System.Diagnostics.Debugger.Launch();
			var options = new DbContextOptionsBuilder<ContextoLibreria>()
								.UseInMemoryDatabase(databaseName: "BaseDatosLibro")
								.Options;

			var contexto = new ContextoLibreria(options);

			var request = new Nuevo.Ejecuta();
			request.Titulo = "LIbro Microservicios";
			request.AutorLibro = Guid.Empty;
			request.FechaPublicacion = DateTime.Now;

			var manejador = new Nuevo.Manejador(contexto);

			var libro = await manejador.Handle(request, new System.Threading.CancellationToken());


			Assert.True(libro != null);


        }
	}
}

