using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Interfaces;
using BibliotecaApi.Infrastructure.Security;

namespace BibliotecaApi.Application.UseCases.Usuarios
{
    public class LoginUseCase
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginUseCase(
            IUsuariosRepository usuariosRepository, 
            IPasswordHasher passwordHasher, 
            ITokenService tokenService)
        {
            _usuariosRepository = usuariosRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDTO> Executar(LoginRequestDTO request)
        {
            var usuario = await _usuariosRepository.ObterPorEmail(request.Email);
            if (usuario == null)
            {
                throw new Exception("E-mail ou senha inválidos.");
            }

            if (!_passwordHasher.Verificar(request.Senha, usuario.SenhaHash))
            {
                throw new Exception("E-mail ou senha inválidos.");
            }

            var token = _tokenService.GenerateToken(usuario);

            return new LoginResponseDTO
            {
                Token = token,
                Nome = usuario.Nome,
                Email = usuario.Email.Endereco,
                NivelAcesso = usuario.NivelAcesso.ToString()
            };
        }
    }
}
