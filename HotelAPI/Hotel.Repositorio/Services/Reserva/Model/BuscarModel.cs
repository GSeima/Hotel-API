using Hotel.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Repositorio.Services.Reserva.Model
{
    public class BuscarModel
    {
        public int ReservaId { get; set; }
        public int QuartoId { get; set; }
        public int Capacidade { get; set; }
        public string Cpf { get; set; }
        public List<HospedeCpf> Hospedes { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
}
