using BibliotecaApi.Domain.Exceptions;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Usuarios
{
    public class ExcluirUsuarioUseCase
    {
        private readonly IUsuariosRepository _repository;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public ExcluirUsuarioUseCase(IUsuariosRepository repository, IEmprestimoRepository emprestimoRepository)
        {
            _repository = repository;
            _emprestimoRepository = emprestimoRepository;
        }

        public async Task ExecuteAsync(int id)
        {
            if (await _emprestimoRepository.TemVinculoComUsuarioAsync(id))
            {
                throw new DomainException("Não é possível excluir o usuário pois ele possui registros de empréstimos vinculados.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
