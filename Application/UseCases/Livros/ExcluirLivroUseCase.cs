using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Livros
{
    public class ExcluirLivroUseCase
    {
        private readonly ILivroRepository _repository;

        public ExcluirLivroUseCase(ILivroRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
