using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Exceptions;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Livros
{
    public class CriarLivroUseCase
    {
        private readonly ILivroRepository _repository;

        public CriarLivroUseCase(ILivroRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(CriarLivroRequest request)
        {
            if (await _repository.ExisteIsbnAsync(request.ISBN))
                throw new DomainException("Livro com este ISBN já cadastrado.");

            var livro = new LivroEntity();
            livro.Cadastrar(null, request.Titulo, request.Autor, request.ISBN);
            livro.Ativar();
            await _repository.AddAsync(livro);
        }
    }
}
