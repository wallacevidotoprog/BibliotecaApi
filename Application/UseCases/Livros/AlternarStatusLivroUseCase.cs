using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Livros
{
    public class AlternarStatusLivroUseCase
    {
        private readonly ILivroRepository _repository;

        public AlternarStatusLivroUseCase(ILivroRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int id, bool ativo)
        {
            var livro = await _repository.GetByIdAsync(id);
            if (livro == null) throw new Exception("Livro não encontrado.");

            if (ativo) livro.Ativar();
            else livro.Desativar();

            await _repository.UpdateAsync(livro);
        }
    }
}
