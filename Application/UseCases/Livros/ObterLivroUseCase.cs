using BibliotecaApi.Application.DTOs;
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

        public async Task<LivroResponse?> ObterPorIdAsync(int id)
        {
            var livro = await _repository.GetByIdAsync(id);
            if (livro == null) return null;

            return MapToResponse(livro);
        }

        public async Task<IEnumerable<LivroResponse>> ObterTodosAsync()
        {
            var livros = await _repository.GetAllAsync();
            return livros.Select(MapToResponse);
        }

        private static LivroResponse MapToResponse(LivroEntity l)
        {
            return new LivroResponse(l.Id, l.Titulo, l.Autor, l.ISBN.Valor, l.Ativo, l.EmUso, l.DataCriacao, l.DataAtualizacao);
        }
    }
}
