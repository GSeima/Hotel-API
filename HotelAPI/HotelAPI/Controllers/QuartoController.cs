using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Repositorio.Services;
using Hotel.Repositorio.Services.Quarto.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuartoController : ControllerBase
    {
        public readonly QuartoService _quarto;

        public QuartoController(QuartoService quartoService)
        {
            _quarto = quartoService;
        }

        [HttpGet]
        public async Task<List<BuscarModel>> Buscar()
        {
            return await _quarto.Buscar();
        }
    }
}
