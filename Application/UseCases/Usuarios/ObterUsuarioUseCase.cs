using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Usuarios
{
    public class ObterUsuarioUseCase
    {
        private readonly IUsuariosRepository _repository;

        public ObterUsuarioUseCase(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        public async Task<UsuariosEntity?> ObterPorIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<UsuariosEntity>> ObterTodosAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
