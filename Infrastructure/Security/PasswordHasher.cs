using BCrypt.Net;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string senha)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(senha);
        }

        public bool Verificar(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(senha, hash);
        }
    }
}
