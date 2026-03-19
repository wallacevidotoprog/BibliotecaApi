using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Usuarios
{
    public class ExcluirUsuarioUseCase
    {
        private readonly IUsuariosRepository _repository;

        public ExcluirUsuarioUseCase(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
