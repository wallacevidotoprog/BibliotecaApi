using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Exceptions;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Emprestimos
{
    public class CriarEmprestimoUseCase
    {
        private readonly IEmprestimoRepository _repository;
        private readonly IUsuariosRepository _usuarioRepository;

        public CriarEmprestimoUseCase(IEmprestimoRepository repository, IUsuariosRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task ExecuteAsync(CriarEmprestimoRequest request)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(request.IdUsuario);
            if (usuario == null) throw new DomainException("Usuário não encontrado.");

            if (usuario.PossuiAtrasoAtivo)
                throw new DomainException("Usuário possui empréstimos em atraso e não pode realizar novos empréstimos.");

            var emprestimo = new EmprestimoEntity();
            emprestimo.Cadastrar(request.IdUsuario, request.IdLivro, request.DataPrevista);
            await _repository.AddAsync(emprestimo);
        }
    }
}
