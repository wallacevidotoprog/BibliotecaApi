using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Usuarios
{
    public class AtualizarUsuarioUseCase
    {
        private readonly IUsuariosRepository _repository;

        public AtualizarUsuarioUseCase(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(AtualizarUsuarioRequest request)
        {
            var usuario = await _repository.GetByIdAsync(request.Id);
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            usuario.Cadastrar(request.Nome, request.CPF, request.Email, request.Senha);
            await _repository.UpdateAsync(usuario);
        }
    }
}
