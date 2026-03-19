using System.Text.RegularExpressions;

namespace BibliotecaApi.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Endereco { get; }

        private Email() { Endereco = string.Empty; } // Required by EF Core

        private Email(string endereco)
        {
            Endereco = endereco;
        }

        public static Email Criar(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não pode ser vazio.");

            email = email.Trim();

            if (!EmailValido(email))
                throw new ArgumentException("Email inválido.");

            return new Email(email);
        }

        private static bool EmailValido(string email)
        {
            // Regex simples e eficiente para validação de email
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        public override string ToString()
        {
            return Endereco;
        }
    }
}
