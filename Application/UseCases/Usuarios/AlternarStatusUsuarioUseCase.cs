using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Usuarios
{
    public class AlternarStatusUsuarioUseCase
    {
        private readonly IUsuariosRepository _repository;

        public AlternarStatusUsuarioUseCase(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int id, bool ativo)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            if (ativo) usuario.Ativar();
            else usuario.Desativar();

            await _repository.UpdateAsync(usuario);
        }
    }
}
