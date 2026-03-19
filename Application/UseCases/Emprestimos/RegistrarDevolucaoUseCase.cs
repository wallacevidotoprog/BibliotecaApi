using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Interfaces;
using BibliotecaApi.Domain.Services;

namespace BibliotecaApi.Application.UseCases.Emprestimos
{
    public class RegistrarDevolucaoUseCase
    {
        private readonly IEmprestimoRepository _repository;
        private readonly IUsuariosRepository _usuarioRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly AtrasoService _atrasoService;

        public RegistrarDevolucaoUseCase(
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

        public async Task<EmprestimoResponse> ExecuteAsync(int id)
        {
            var emprestimo = await _repository.GetByIdAsync(id);
            if (emprestimo == null) throw new Exception("Empréstimo não encontrado.");

            emprestimo.RegistrarDevolucao();
            await _repository.UpdateAsync(emprestimo);

            var usuario = emprestimo.Usuario;
            if (usuario != null)
            {
                var emprestimos = await _repository.GetByUsuarioIdAsync(usuario.Id);
                _atrasoService.AtualizarUsuario(usuario, emprestimos.ToList());
                await _usuarioRepository.UpdateAsync(usuario);
            }
            var livro = await _livroRepository.GetByIdAsync(emprestimo.IdLivro);
            if (livro != null)
            {
                livro.DesmarcarComoEmUso();
                await _livroRepository.UpdateAsync(livro);
            }

            return new EmprestimoResponse(
                emprestimo.Id,
                new UsuarioEmprestimoResponse(usuario!.Id, usuario.Nome),
                new LivroEmprestimoResponse(livro!.Id, livro.Titulo, livro.ISBN.Valor),
                emprestimo.DataEmprestimo,
                emprestimo.DataPrevistaDevolucao,
                emprestimo.DataDevolucao,
                emprestimo.Valor,
                emprestimo.Multa,
                emprestimo.Total,
                emprestimo.DataCriacao,
                emprestimo.DataAtualizacao
            );
        }
    }
}
