using System;
namespace TiendaServicios.Api.Gateway.RemoteModel
{
	public class AutorModeloRemote
	{
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTimeOffset? FechaNacimiento { get; set; }
		public string AutorLibroGuid { get; set; }
	}
}

