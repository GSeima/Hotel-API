using System;

namespace Hotel.Repositorio.Services.Cliente.Model
{
    public class CadastrarModel
    {
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public void ValidarCadastro()
        {
            if (string.IsNullOrWhiteSpace(Cpf))
                throw new Exception("CPF obrigatório.");

            if (Cpf.Length != 11)
                throw new Exception("CPF inválido.");

            if (string.IsNullOrWhiteSpace(NomeCompleto))
                throw new Exception("Nome obrigatório.");

            if (DataNascimento == null)
                throw new Exception("Data de Nascimento obrigatório.");

            if (DateTime.Now.Year - DataNascimento.Year < 18)
                throw new Exception("O cliente precisa ter mais de 18 anos para se cadastrar.");

            if (string.IsNullOrWhiteSpace(Email))
                throw new Exception("Email obrigatório.");

            if (string.IsNullOrWhiteSpace(Telefone))
                throw new Exception("Telefone obrigatório.");

            if (Telefone.Length != 11)
                throw new Exception("Telefone inválido.");
        }
    }
}
