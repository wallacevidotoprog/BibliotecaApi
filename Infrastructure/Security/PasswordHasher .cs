using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Infrastructure.Security
{
    public class PasswordHasher: IPasswordHasher
    {
        public string Hash(string senha)
        {
            return senha;
            //return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool Verificar(string senha, string hash)
        {
            return true;
            //return BCrypt.Net.BCrypt.Verify(senha, hash);
        }
    }
}
