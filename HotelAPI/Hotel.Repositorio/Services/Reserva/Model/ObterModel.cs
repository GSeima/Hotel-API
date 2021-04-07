using Hotel.Dominio.Entities;
using System;
using System.Collections.Generic;

namespace Hotel.Repositorio.Services.Reserva.Model
{
    public class ObterModel
    {
        public int ReservaId { get; set; }
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public Dominio.Entities.Cliente Cliente { get; set; }
        public List<HospedeCpf> Hospedes { get; set; }
        public int QuartoId { get; set; }
        public DateTime DataCriacaoReserva { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public decimal? ValorTotal { get; set; }
    }
}
