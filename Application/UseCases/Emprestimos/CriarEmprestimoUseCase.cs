using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Emprestimos
{
    public class CriarEmprestimoUseCase
    {
        private readonly IEmprestimoRepository _repository;

        public CriarEmprestimoUseCase(IEmprestimoRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(CriarEmprestimoRequest request)
        {
            var emprestimo = new EmprestimoEntity();
            emprestimo.Cadastrar(request.IdUsuario, request.IdLivro, request.DataPrevista);
            await _repository.AddAsync(emprestimo);
        }
    }
}
