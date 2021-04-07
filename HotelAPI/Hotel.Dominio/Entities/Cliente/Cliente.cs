using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Dominio.Entities
{
    public class Cliente
    {
        [Key]
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCriacaoCliente { get; set; }
    }
}
