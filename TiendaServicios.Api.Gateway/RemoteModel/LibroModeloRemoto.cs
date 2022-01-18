using System;
namespace TiendaServicios.Api.Gateway.RemoteModel
{
	public class LibroModeloRemoto
	{
		public Guid? LibreriaMaterialId { get; set; }
		public string Titulo { get; set; }
		public DateTimeOffset? FechaPublicacion { get; set; }
		public Guid? AutorLibro { get; set; }
		public AutorModeloRemote AutoData { get; set; }
	}
}

