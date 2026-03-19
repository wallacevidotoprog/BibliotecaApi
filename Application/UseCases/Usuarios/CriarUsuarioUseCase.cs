using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Exceptions;
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
            if (await _repository.ExisteCpfAsync(request.CPF))
                throw new DomainException("Usuário com este CPF já cadastrado.");

            if (await _repository.ExisteEmailAsync(request.Email))
                throw new DomainException("Usuário com este Email já cadastrado.");

            var usuario = new UsuariosEntity();
            usuario.Cadastrar(request.Nome, request.CPF, request.Email, request.Senha, request.NivelAcesso);
            await _repository.AddAsync(usuario);
        }
    }
}
