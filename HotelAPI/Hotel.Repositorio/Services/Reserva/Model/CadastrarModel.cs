using Hotel.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Repositorio.Services.Reserva.Model
{
    public class CadastrarModel
    {
        public string Cpf { get; set; }
        public int QuartoId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public List<HospedeCpf> Hospedes { get; set; }

        public void ValidarCadastro()
        {
            if (string.IsNullOrWhiteSpace(Cpf))
                throw new Exception("CPF obrigatório.");

            if (Cpf.Length != 11)
                throw new Exception("CPF inválido.");

            if (QuartoId > 50 || QuartoId < 1)
                throw new Exception("Número do quarto inválido.");

            if (DataEntrada > DataSaida)
                throw new Exception("Data de entrada não pode ser  maior que a Data de saida.");
        }
    }
}
