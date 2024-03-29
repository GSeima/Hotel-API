﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Dominio.Entities;
using Hotel.Repositorio.Services;
using Hotel.Repositorio.Services.Reserva.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        public readonly ReservaService _reserva;

        public ReservaController(ReservaService reservaService)
        {
            _reserva = reservaService;
        }

        [HttpGet]
        public async Task<List<BuscarModel>> Buscar()
        {
            return await _reserva.Buscar();
        }

        [HttpGet("{reservaId}")]
        public async Task<ObterModel> Obter(int reservaId)
        {
            return await _reserva.Obter(reservaId);
        }

        [HttpPost("cadastro")]
        public async Task Cadastrar([FromBody] CadastrarModel adicionar)
        {
            await _reserva.Cadastrar(adicionar);
        }

        [HttpPost("{reservaId}/cancelar")]
        public async Task Cancelar(int reservaId)
        {
            await _reserva.Cancelar(reservaId);
        }

        [HttpPost("{reservaId}/checkIn")]
        public async Task CheckIn(int reservaId, [FromBody] List<HospedeCpf> hospedes)
        {
            await _reserva.CheckIn(reservaId, hospedes);
        }

        [HttpPost("{reservaId}/checkOut")]
        public async Task CheckOut(int reservaId, [FromBody] CheckOutModel checkOut)
        {
            await _reserva.CheckOut(reservaId, checkOut);
        }
    }
}
