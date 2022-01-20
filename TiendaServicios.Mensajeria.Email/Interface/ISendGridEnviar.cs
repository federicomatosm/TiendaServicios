using System;
using TiendaServicios.Mensajeria.Email.Modelo;

namespace TiendaServicios.Mensajeria.Email.Interface
{
	public interface ISendGridEnviar
	{

		Task<(bool resultado, string errorMessage)> EnviarEmail(SendGridData data);
	}
}

