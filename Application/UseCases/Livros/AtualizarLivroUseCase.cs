using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Exceptions;
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
            if (livro == null) throw new DomainException("Livro não encontrado.");

            var titulo = !string.IsNullOrWhiteSpace(request.Titulo) ? request.Titulo : livro.Titulo;
            var autor = !string.IsNullOrWhiteSpace(request.Autor) ? request.Autor : livro.Autor;
            var isbn = !string.IsNullOrWhiteSpace(request.ISBN) ? request.ISBN : livro.ISBN.Valor;
            if (await _repository.ExisteIsbnExceptIdAsync(isbn, request.Id))
                throw new DomainException("Já existe outro livro cadastrado com este ISBN.");

            livro.Cadastrar(request.Id, titulo, autor, isbn);
            await _repository.UpdateAsync(livro);
        }
    }
}
