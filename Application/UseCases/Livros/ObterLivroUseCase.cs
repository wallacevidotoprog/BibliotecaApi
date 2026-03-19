using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Livros
{
    public class ObterLivroUseCase
    {
        private readonly ILivroRepository _repository;

        public ObterLivroUseCase(ILivroRepository repository)
        {
            _repository = repository;
        }

        public async Task<LivroEntity?> ObterPorIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<LivroEntity>> ObterTodosAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
