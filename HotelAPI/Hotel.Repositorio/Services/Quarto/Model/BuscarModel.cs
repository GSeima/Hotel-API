using Hotel.Dominio.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Repositorio.Services.Quarto.Model
{
    public class BuscarModel
    {
        public int QuartoId { get; set; }
        public string TipoDescricao { get; set; }
        public string SituacaoDescricao { get; set; }
    }
}
