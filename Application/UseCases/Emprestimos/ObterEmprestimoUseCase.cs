using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Emprestimos
{
    public class ObterEmprestimoUseCase
    {
        private readonly IEmprestimoRepository _repository;

        public ObterEmprestimoUseCase(IEmprestimoRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmprestimoEntity?> ObterPorIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<EmprestimoEntity>> ObterTodosAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
