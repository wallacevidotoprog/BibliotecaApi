using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;
using BibliotecaApi.Domain.Services;

namespace BibliotecaApi.Application.UseCases.Usuarios
{
    public class ObterUsuarioUseCase
    {
        private readonly IUsuariosRepository _repository;
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly AtrasoService _atrasoService;

        public ObterUsuarioUseCase(
            IUsuariosRepository repository, 
            IEmprestimoRepository emprestimoRepository,
            AtrasoService atrasoService)
        {
            _repository = repository;
            _emprestimoRepository = emprestimoRepository;
            _atrasoService = atrasoService;
        }

        public async Task<UsuarioResponse?> ObterPorIdAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return null;

            await AtualizarAtrasoAsync(usuario);

            return MapToResponse(usuario);
        }

        public async Task<IEnumerable<UsuarioResponse>> ObterTodosAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            var responses = new List<UsuarioResponse>();

            foreach (var user in usuarios)
            {
                await AtualizarAtrasoAsync(user);
                responses.Add(MapToResponse(user));
            }

            return responses;
        }

        private async Task AtualizarAtrasoAsync(UsuariosEntity usuario)
        {
            var emprestimos = await _emprestimoRepository.GetByUsuarioIdAsync(usuario.Id);
            _atrasoService.AtualizarUsuario(usuario, emprestimos.ToList());
            await _repository.UpdateAsync(usuario);
        }

        private static UsuarioResponse MapToResponse(UsuariosEntity u)
        {
            return new UsuarioResponse(
                u.Id, 
                u.Nome, 
                u.CPF.Numero, 
                u.Email.Endereco, 
                u.NivelAcesso, 
                u.Ativo, 
                u.PossuiAtrasoAtivo,
                u.DataCriacao, 
                u.DataAtualizacao);
        }
    }
}
