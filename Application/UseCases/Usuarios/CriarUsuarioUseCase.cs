using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Enums;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Usuarios
{
    public class CriarUsuarioUseCase
    {
        private readonly IUsuariosRepository _repository;

        public CriarUsuarioUseCase(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(CriarUsuarioRequest request)
        {
            var usuario = new UsuariosEntity();
            usuario.Cadastrar(request.Nome, request.CPF, request.Email, request.Senha, request.NivelAcesso);
            await _repository.AddAsync(usuario);
        }
    }
}
