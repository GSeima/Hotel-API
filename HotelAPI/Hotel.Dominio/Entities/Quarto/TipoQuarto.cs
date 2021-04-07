using Hotel.Dominio.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hotel.Dominio.Entities
{
    public class TipoQuarto
    {
        [Key]
        public Tipo TipoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Capacidade { get; set; }
    }
}
