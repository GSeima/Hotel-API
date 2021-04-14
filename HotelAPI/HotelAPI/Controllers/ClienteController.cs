using Hotel.Repositorio.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Repositorio.Services;
using Hotel.Repositorio.Services.Cliente.Model;

namespace HotelAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public readonly ClienteService _cliente;

        public ClienteController(ClienteService clienteService)
        {
            _cliente = clienteService;
        }

        [HttpGet]
        public async Task<List<BuscarModel>> Buscar()
        {
            return await _cliente.Buscar();
        }

        [HttpGet("{cpf}")]
        public async Task<ObterModel> Obter(string cpf)
        {
            return await _cliente.Obter(cpf);
        }

        [HttpPost("cadastro")]
        public async Task Cadastrar([FromBody] CadastrarModel cadastro)
        {
            await _cliente.Cadastrar(cadastro);
        }

        [HttpPut("{cpf}/editar")]
        public async Task Editar(string cpf, [FromBody] EditarModel editar)
        {
            await _cliente.Editar(cpf, editar);
        }
    }
}
