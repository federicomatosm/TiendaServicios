﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.API.CarritoCompra.Aplicacion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaServicios.API.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoComprasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarritoComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]

        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id)
        {
            return await _mediator.Send(new Consulta.Ejecuta { CarritoSessionId = id });

        }
    }
}
