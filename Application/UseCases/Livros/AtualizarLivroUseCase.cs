using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Livros
{
    public class AtualizarLivroUseCase
    {
        private readonly ILivroRepository _repository;

        public AtualizarLivroUseCase(ILivroRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(AtualizarLivroRequest request)
        {
            var livro = await _repository.GetByIdAsync(request.Id);
            if (livro == null) throw new Exception("Livro não encontrado.");

            livro.Cadastrar(request.Id, request.Titulo, request.Autor, request.ISBN);
            await _repository.UpdateAsync(livro);
        }
    }
}
