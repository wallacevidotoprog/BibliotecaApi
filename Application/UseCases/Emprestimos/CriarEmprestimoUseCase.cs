using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Exceptions;
using BibliotecaApi.Domain.Interfaces;
using BibliotecaApi.Domain.Services;

namespace BibliotecaApi.Application.UseCases.Emprestimos
{
    public class CriarEmprestimoUseCase
    {
        private readonly IEmprestimoRepository _repository;
        private readonly IUsuariosRepository _usuarioRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly AtrasoService _atrasoService;

        public CriarEmprestimoUseCase(
            IEmprestimoRepository repository, 
            IUsuariosRepository usuarioRepository,
            ILivroRepository livroRepository,
            AtrasoService atrasoService)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _livroRepository = livroRepository;
            _atrasoService = atrasoService;
        }

        public async Task ExecuteAsync(CriarEmprestimoRequest request)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(request.IdUsuario);
            if (usuario == null) throw new DomainException("Usuário não encontrado.");

            if (!usuario.Ativo)
                throw new DomainException("Não é possível realizar empréstimo para um usuário desativado.");

            // Validação proativa de atrasos
            var emprestimosDoUsuario = await _repository.GetByUsuarioIdAsync(usuario.Id);
            _atrasoService.AtualizarUsuario(usuario, emprestimosDoUsuario.ToList());
            
            if (usuario.PossuiAtrasoAtivo)
            {
                await _usuarioRepository.UpdateAsync(usuario);
                throw new DomainException("Usuário com empréstimo em atraso não pode realizar novo empréstimo.");
            }

            var livro = await _livroRepository.GetByIdAsync(request.IdLivro);
            if (livro == null) throw new DomainException("Livro não encontrado.");

            if (!livro.Ativo)
                throw new DomainException("Livro não está ativo no sistema.");

            if (livro.EmUso)
                throw new DomainException("Este livro já está emprestado e ainda não foi devolvido.");


            var emprestimo = new EmprestimoEntity();
            emprestimo.Cadastrar(request.IdUsuario, request.IdLivro, request.DataPrevista);
            await _repository.AddAsync(emprestimo);
            livro.MarcarComoEmUso();
            await _livroRepository.UpdateAsync(livro);
        }
    }
}
