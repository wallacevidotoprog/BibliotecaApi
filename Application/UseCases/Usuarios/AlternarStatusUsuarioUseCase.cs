using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Usuarios
{
    public class AlternarStatusUsuarioUseCase
    {
        private readonly IUsuariosRepository _repository;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public AlternarStatusUsuarioUseCase(IUsuariosRepository repository, IEmprestimoRepository emprestimoRepository)
        {
            _repository = repository;
            _emprestimoRepository = emprestimoRepository;
        }

        public async Task ExecuteAsync(int id, bool ativo)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) throw new BibliotecaApi.Domain.Exceptions.DomainException("Usuário não encontrado.");

            if (!ativo && await _emprestimoRepository.TemEmprestimoPendentePorUsuarioAsync(id))
            {
                throw new BibliotecaApi.Domain.Exceptions.DomainException("Não é possível desativar o usuário pois ele possui empréstimos pendentes.");
            }

            if (ativo) usuario.Ativar();
            else usuario.Desativar();

            await _repository.UpdateAsync(usuario);
        }
    }
}
