using System;
using TiendaServicios.Api.Gateway.RemoteModel;

namespace TiendaServicios.Api.Gateway.RemoteInterface
{
	public interface IAutorRemote
	{
		Task<(bool resultado, AutorModeloRemote autor, string ErrorMessage)> GetAutor(Guid id);
	}
}

