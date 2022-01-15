using System;
using System.Threading.Tasks;
using TiendaServicios.API.CarritoCompra.ModelosRemotos;

namespace TiendaServicios.API.CarritoCompra.InterfacesRemota
{
    public interface IServicioLibro
    {

        Task<(bool resultado, LibroRemoto libro, string ErrorMessage)> GetLibro(Guid libroId);
    }
}
