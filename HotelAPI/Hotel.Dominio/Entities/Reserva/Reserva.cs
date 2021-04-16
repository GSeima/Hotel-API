using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Dominio.Entities
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }

        // FK
        public string Cpf { get; set; }
        [ForeignKey(nameof(Cpf))]
        public Cliente Cliente { get; set; }

        [NotMapped]
        public List<HospedeCpf> Hospedes { get; set; }

        public string HospedesJson
        {
            get
            {
                if (this.Hospedes == null)
                    return null;

                return JsonConvert.SerializeObject(Hospedes);
            }
            set
            {
                this.Hospedes = null;

                if (value != null)
                {
                    var tmp = JsonConvert.DeserializeObject<List<HospedeCpf>>(value);
                    this.Hospedes = tmp;
                }
            }
        }

        // FK
        public int QuartoId { get; set; }
        [ForeignKey(nameof(QuartoId))]
        public Quarto Quarto { get; set; }

        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }

        public DateTime DataCriacaoReserva { get; set; }
        public DateTime? CheckIn { get; set; }
        public decimal? ValorDiarias { get; set; }
        public decimal? Multa { get; set; }
        public decimal? TaxasConsumo { get; set; }
        public decimal? ValorTotal { get; set; }
        public DateTime? CheckOut { get; set; }

        // FK
        public Status StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public StatusReserva StatusReserva { get; set; }

        public Reserva()
        {
            this.Hospedes = Hospedes;
        }
    }
}
