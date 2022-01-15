﻿using System;
using System.Collections.Generic;

namespace TiendaServicios.API.CarritoCompra.Aplicacion
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDto> ListaProducto { get; set; }
    }
}
