using System;

namespace Hotel.Repositorio.Services.Cliente.Model
{
    public class ObterModel
    {
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCriacaoCliente { get; set; }
    }
}
