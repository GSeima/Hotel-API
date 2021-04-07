using Hotel.Dominio.Entities.Enums;
using Hotel.Repositorio.Data;
using Hotel.Repositorio.Services.Quarto.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Repositorio.Services
{
    public class QuartoService
    {
        public readonly HotelContext _context;

        public QuartoService(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<BuscarModel>> Buscar()
        {
            var quarto = await _context
                .Quarto
                .Where(q => q.SituacaoId != Situacao.Disponivel)
                .Select(q => new BuscarModel 
            {
                QuartoId = q.QuartoId,
                TipoDescricao = q.TipoQuarto.Descricao,
                SituacaoDescricao = q.SituacaoQuarto.Descricao
            }).ToListAsync();

            return quarto;
        }
    }
}
