using BibliotecaApi.Domain.Exceptions;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Livros
{
    public class ExcluirLivroUseCase
    {
        private readonly ILivroRepository _repository;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public ExcluirLivroUseCase(ILivroRepository repository, IEmprestimoRepository emprestimoRepository)
        {
            _repository = repository;
            _emprestimoRepository = emprestimoRepository;
        }

        public async Task ExecuteAsync(int id)
        {
            if (await _emprestimoRepository.TemVinculoComLivroAsync(id))
            {
                throw new DomainException("Não é possível excluir o livro pois ele possui registros de empréstimos vinculados.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
