using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dominio.Entities
{
    public class StatusReserva
    {
        [Key]
        public Status StatusId { get; set; }
        public string Descricao { get; set; }
    }
}
