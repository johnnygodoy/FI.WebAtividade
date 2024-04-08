using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FI.AtividadeEntrevista.DML
{
    public class Cliente
    {
        public long Id { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public string Logradouro { get; set; }
        public string Nacionalidade { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }

        private string _cpf;
        public string CPF
        {
            get => _cpf;
            set
            {
                if (!IsValidCPF(value))
                {
                    throw new ArgumentException("CPF inválido.");
                }
                _cpf = value;
            }
        }

        private bool IsValidCPF(string cpf)
        {
            cpf = cpf?.Replace(".", "").Replace("-", "");

            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;

            int[] multiplicadoresPrimeiroDigito = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadoresSegundoDigito = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma, resto;

            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadoresPrimeiroDigito[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadoresSegundoDigito[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

    }
}
