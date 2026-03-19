using System.Text.RegularExpressions;

namespace BibliotecaApi.Domain.ValueObjects
{
    public sealed record ISBN
    {
        public string Valor { get; }

        private ISBN() { Valor = string.Empty; }

        private ISBN(string numero)
        {
            Valor = numero;
        }
        public static ISBN Criar(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN não pode ser vazio");

            string? somenteNumeros = Regex.Replace(isbn, @"\D", "");

            if (somenteNumeros.Length != 13)
                throw new ArgumentException("ISBN deve conter 13 dígitos");

            if (!EhValido(somenteNumeros))
                throw new ArgumentException("ISBN inválido");

            return new ISBN(somenteNumeros);
        }
        private static bool EhValido(string isbn)
        {
            int soma = 0;

            for (int i = 0; i < 12; i++)
            {
                int num = int.Parse(isbn[i].ToString());
                soma += (i % 2 == 0) ? num : num * 3;
            }

            int resto = soma % 10;
            int digito = (resto == 0) ? 0 : 10 - resto;

            return digito == int.Parse(isbn[12].ToString());
        }

        public override string ToString()
        {
            return Valor;
        }
    }
}
