using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Emprestimos
{
    public class RegistrarDevolucaoUseCase
    {
        private readonly IEmprestimoRepository _repository;

        public RegistrarDevolucaoUseCase(IEmprestimoRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int id)
        {
            var emprestimo = await _repository.GetByIdAsync(id);
            if (emprestimo == null) throw new Exception("Empréstimo não encontrado.");

            emprestimo.RegistrarDevolucao();
            await _repository.UpdateAsync(emprestimo);
        }
    }
}
