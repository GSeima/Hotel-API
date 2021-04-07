using Hotel.Repositorio.Data;
using Hotel.Repositorio.Services.Cliente.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repositorio.Services
{
    public class ClienteService
    {
        public readonly HotelContext _context;

        public ClienteService(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<BuscarModel>> Buscar()
        {
            var clientes = await _context
                .Cliente
                .Select(
                c => new BuscarModel
                {
                    Cpf = c.Cpf,
                    NomeCompleto = c.NomeCompleto,
                    Email = c.Email,
                    Telefone = c.Telefone
                }).ToListAsync();

            return clientes;
        }

        public async Task<ObterModel> Obter(string cpf)
        {
            var cliente = await _context
                .Cliente
                .Where(c => c.Cpf == cpf)
                .Select(
                c => new ObterModel
                {
                    Cpf = c.Cpf,
                    NomeCompleto = c.NomeCompleto,
                    DataNascimento = c.DataNascimento,
                    Email = c.Email,
                    Telefone = c.Telefone,
                    DataCriacaoCliente = c.DataCriacaoCliente
                }).FirstOrDefaultAsync();

            if (cliente == null)
                throw new Exception("CPF não encontrado.");

            return cliente;
        }

        public async Task Cadastrar([FromBody] CadastrarModel model)
        {
            model.ValidarCadastro();

            var verificaCpf = await _context
                .Cliente
                .AnyAsync(c => c.Cpf == model.Cpf);

            if (verificaCpf)
                throw new Exception("CPF já cadastrado.");

            var cliente = new Dominio.Entities.Cliente()
            {
                Cpf = model.Cpf,
                NomeCompleto = model.NomeCompleto,
                DataNascimento = model.DataNascimento,
                Email = model.Email,
                Telefone = model.Telefone,
                DataCriacaoCliente = DateTime.Now
            };

            _context.Cliente.Add(cliente);

            await _context.SaveChangesAsync();
        }
    }
}
