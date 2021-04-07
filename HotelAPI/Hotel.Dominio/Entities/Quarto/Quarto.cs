using Hotel.Dominio.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hotel.Dominio.Entities
{
    public class Quarto
    {
        [Key]
        public int QuartoId { get; set; }

        // FK
        public Tipo TipoId { get; set; }
        [ForeignKey(nameof(TipoId))]
        public TipoQuarto TipoQuarto { get; set; }

        // FK
        public Situacao SituacaoId { get; set; }
        [ForeignKey(nameof(SituacaoId))]
        public SituacaoQuarto SituacaoQuarto { get; set; }
    }
}
