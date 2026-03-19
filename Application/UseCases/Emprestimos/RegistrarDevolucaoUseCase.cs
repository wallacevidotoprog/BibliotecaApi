using BibliotecaApi.Domain.Interfaces;
using BibliotecaApi.Domain.Services;

namespace BibliotecaApi.Application.UseCases.Emprestimos
{
    public class RegistrarDevolucaoUseCase
    {
        private readonly IEmprestimoRepository _repository;
        private readonly IUsuariosRepository _usuarioRepository;
        private readonly AtrasoService _atrasoService;

        public RegistrarDevolucaoUseCase(
            IEmprestimoRepository repository, 
            IUsuariosRepository usuarioRepository,
            AtrasoService atrasoService)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _atrasoService = atrasoService;
        }

        public async Task ExecuteAsync(int id)
        {
            var emprestimo = await _repository.GetByIdAsync(id);
            if (emprestimo == null) throw new Exception("Empréstimo não encontrado.");

            emprestimo.RegistrarDevolucao();
            await _repository.UpdateAsync(emprestimo);

            var usuario = await _usuarioRepository.GetByIdAsync(emprestimo.IdUsuario);
            if (usuario != null)
            {
                var emprestimos = await _repository.GetByUsuarioIdAsync(usuario.Id);
                _atrasoService.AtualizarUsuario(usuario, emprestimos.ToList());
                await _usuarioRepository.UpdateAsync(usuario);
            }
        }
    }
}
