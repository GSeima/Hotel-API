using Hotel.Dominio.Entities;
using Hotel.Dominio.Entities.Enums;
using Hotel.Repositorio.Data;
using Hotel.Repositorio.Services.Reserva.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Repositorio.Services
{
    public class ReservaService
    {
        public readonly HotelContext _context;
        public QuartoService _quarto;

        public ReservaService(HotelContext context, QuartoService quarto)
        {
            _context = context;
            _quarto = quarto;
        }

        public async Task<List<BuscarModel>> Buscar()
        {
            var reservas = await _context
                .Reserva
                .Select(
                r => new BuscarModel
                {
                    ReservaId = r.ReservaId,
                    QuartoId = r.QuartoId,
                    Capacidade = r.Quarto.TipoQuarto.Capacidade,
                    Cpf = r.Cpf,
                    CheckIn = r.CheckIn,
                    CheckOut = r.CheckOut
                    
                }).ToListAsync();

            return reservas;
        }

        public async Task<ObterModel> Obter(int reservaId)
        {
            var reserva = await _context
                .Reserva
                .Where(r => r.ReservaId == reservaId)
                .Select(
                r => new ObterModel
                {
                    ReservaId = r.ReservaId,
                    Cpf = r.Cpf,
                    NomeCompleto = r.Cliente.NomeCompleto,
                    Hospedes = r.Hospedes,
                    QuartoId = r.QuartoId,
                    DataCriacaoReserva = r.DataCriacaoReserva,
                    DataEntrada = r.DataEntrada,
                    DataSaida = r.DataSaida,
                    CheckIn = r.CheckIn.Value,
                    CheckOut = r.CheckOut.Value,
                    ValorTotal = r.ValorTotal.Value
                }).FirstOrDefaultAsync();

            return reserva;
        }

        public async Task Cadastrar([FromBody] CadastrarModel model)
        {
            model.ValidarCadastro();

            var verificaCpf = await _context
                .Cliente
                .AnyAsync(c => c.Cpf == model.Cpf);

            if (!verificaCpf)
                throw new Exception("CPF não cadastrado.");

            var verificaData = await _context
                .Reserva
                .Where(q => q.QuartoId == model.QuartoId)
                .Where(r => r.CheckOut == null)
                .Where(d => d.DataSaida > model.DataEntrada && d.DataEntrada < model.DataSaida)
                .FirstOrDefaultAsync();

            if (verificaData != null)
                throw new Exception($"Quarto {model.QuartoId} estará reservado nesta data.");

            var quarto = await _context
                .Quarto
                .Where(q => q.QuartoId == model.QuartoId)
                .FirstOrDefaultAsync();

            var reserva = new Dominio.Entities.Reserva
            {
                Cpf = model.Cpf,
                QuartoId = model.QuartoId,
                DataEntrada = model.DataEntrada,
                DataSaida = model.DataSaida,
                Hospedes = new List<HospedeCpf>(),
                DataCriacaoReserva = DateTime.Now
            };

            _context.Reserva.Add(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task CheckIn(int reservaId, List<HospedeCpf> hospedes)
        {
            var reserva = await _context
                    .Reserva
                    .Where(r => r.ReservaId == reservaId)
                    .Include(q => q.Quarto.TipoQuarto)
                    .FirstOrDefaultAsync();

            if (reserva == null)
                throw new Exception("Reserva não cadastrada.");

            if (reserva.DataEntrada > DateTime.Now)
                throw new Exception("Check-In não pode ser feito antes da data de entrada.");

            if (reserva.DataEntrada.Date < DateTime.Now.Date)
                throw new Exception($"A reserva foi cancelada, a data de entrada {reserva.DataEntrada} foi excedida.");

            if (reserva.CheckIn != null)
                throw new Exception("Check-In já cadastrado.");

            if (hospedes.Count > reserva.Quarto.TipoQuarto.Capacidade)
                throw new Exception("Número de hospedes maior que a capacidade do quarto.");

            hospedes.ForEach(a =>
            {
                var cliente = _context
                   .Cliente
                   .Where(c => c.Cpf == a.Cpf)
                   .FirstOrDefault();

                if (cliente == null)
                    throw new Exception($"CPF {a.Cpf} não cadastrado.");
            });

            reserva.Hospedes = hospedes;
            reserva.CheckIn = DateTime.Now;
            reserva.Quarto.SituacaoId = Situacao.Ocupado;

            await _context.SaveChangesAsync();
        }

        public async Task<ObterModel> CheckOut(int reservaId, CheckOutModel model)
        {
            var reserva = await _context
                .Reserva
                .Where(r => r.ReservaId == reservaId)
                .Include(q => q.Quarto.TipoQuarto)
                .FirstOrDefaultAsync();

            if (reserva == null)
                throw new Exception("Reserva não cadastrada.");

            if (reserva.CheckIn == null)
                throw new Exception("Check-In não foi realizado.");

            if (reserva.CheckOut != null)
                throw new Exception("Check-Out já cadastrado.");

            reserva.TaxasConsumo = model.TaxasConsumo;
            reserva.ValorDiarias = reserva.Quarto.TipoQuarto.Valor;
            reserva.CheckOut = DateTime.Now;

            var tempoHospedagem = (reserva.CheckOut.Value - reserva.CheckIn.Value);

            if (reserva.DataSaida < DateTime.Now.Date)
            {
                reserva.Multa = reserva.Quarto.TipoQuarto.Valor;
            }
            else
            {
                reserva.Multa = 0;
            }

            if (tempoHospedagem.TotalDays <= 1)
            {
                reserva.ValorTotal = reserva.ValorDiarias + reserva.TaxasConsumo + reserva.Multa;
            }
            else
            {
                reserva.ValorTotal = ((int)Math.Ceiling(tempoHospedagem.TotalDays) * reserva.ValorDiarias) + reserva.TaxasConsumo + reserva.Multa;
            }

            reserva.Quarto.SituacaoId = Situacao.Disponivel;

            await _context.SaveChangesAsync();

            return await Obter(reservaId);
        }
    }
}