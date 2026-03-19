using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Emprestimos
{
    public class ExcluirEmprestimoUseCase
    {
        private readonly IEmprestimoRepository _repository;

        public ExcluirEmprestimoUseCase(IEmprestimoRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
