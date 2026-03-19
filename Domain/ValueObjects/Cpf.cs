using System.Text.RegularExpressions;

namespace BibliotecaApi.Domain.ValueObjects
{
    public sealed record CPF
    {
        public string Numero { get; }

        private CPF() { Numero = string.Empty; } // Required by EF Core

        private CPF(string numero)
        {
            Numero = numero;
        }

        public static CPF Criar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser vazio.");

            string? somenteNumeros = Regex.Replace(cpf, @"\D", "");

            if (somenteNumeros.Length != 11)
                throw new ArgumentException("CPF deve conter 11 dígitos.");

            return new CPF(somenteNumeros);
        }
        public override string ToString()
        {
            return Numero;
        }
    }
}
