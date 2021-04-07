using Hotel.Dominio.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hotel.Dominio.Entities
{
    public class SituacaoQuarto
    {
        [Key]
        public Situacao SituacaoId { get; set; }
        public string Descricao { get; set; }
    }
}
