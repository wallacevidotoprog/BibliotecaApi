using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Exceptions;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Usuarios
{
    public class AtualizarUsuarioUseCase
    {
        private readonly IUsuariosRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public AtualizarUsuarioUseCase(IUsuariosRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task ExecuteAsync(AtualizarUsuarioRequest request)
        {
            var usuario = await _repository.GetByIdAsync(request.Id);
            if (usuario == null) throw new DomainException("Usuário não encontrado.");


            var nome = !string.IsNullOrWhiteSpace(request.Nome) ? request.Nome : usuario.Nome;
            var cpf = !string.IsNullOrWhiteSpace(request.CPF) ? request.CPF : usuario.CPF.Numero;
            var email = !string.IsNullOrWhiteSpace(request.Email) ? request.Email : usuario.Email.Endereco;
            var senha = !string.IsNullOrWhiteSpace(request.Senha) 
                ? _passwordHasher.Hash(request.Senha) 
                : usuario.SenhaHash;

            if (await _repository.ExisteCpfExceptIdAsync(cpf, request.Id))
                throw new DomainException("Já existe outro usuário cadastrado com este CPF.");

            if (await _repository.ExisteEmailExceptIdAsync(email, request.Id))
                throw new DomainException("Já existe outro usuário cadastrado com este Email.");

            usuario.Cadastrar(nome, cpf, email, senha, usuario.NivelAcesso);
            await _repository.UpdateAsync(usuario);
        }
    }
}
