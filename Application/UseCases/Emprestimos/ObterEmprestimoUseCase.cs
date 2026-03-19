using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Interfaces;

namespace BibliotecaApi.Application.UseCases.Emprestimos
{
    public class ObterEmprestimoUseCase
    {
        private readonly IEmprestimoRepository _repository;

        public ObterEmprestimoUseCase(IEmprestimoRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmprestimoResponse?> ObterPorIdAsync(int id)
        {
            var e = await _repository.GetByIdAsync(id);
            if (e == null) return null;

            return MapToResponse(e);
        }

        public async Task<IEnumerable<EmprestimoResponse>> ObterTodosAsync()
        {
            var emprestimos = await _repository.GetAllAsync();
            return emprestimos.Select(MapToResponse);
        }

        private static EmprestimoResponse MapToResponse(EmprestimoEntity e)
        {
            var usuarioResponse = e.Usuario != null 
                ? new UsuarioResponse(e.Usuario.Id, e.Usuario.Nome, e.Usuario.CPF.Numero, e.Usuario.Email.Endereco, e.Usuario.NivelAcesso.ToString(), e.Usuario.Ativo, e.Usuario.PossuiAtrasoAtivo, e.Usuario.DataCriacao, e.Usuario.DataAtualizacao)
                : null;

            var livroResponse = e.Livro != null
                ? new LivroResponse(e.Livro.Id, e.Livro.Titulo, e.Livro.Autor, e.Livro.ISBN.Valor, e.Livro.Ativo, e.Livro.EmUso, e.Livro.DataCriacao, e.Livro.DataAtualizacao)
                : null;

            return new EmprestimoResponse(
                e.Id,
                new UsuarioEmprestimoResponse(e.Usuario.Id, e.Usuario.Nome),
                new LivroEmprestimoResponse(e.Livro.Id, e.Livro.Titulo, e.Livro.ISBN.Valor),
                e.DataEmprestimo,
                e.DataPrevistaDevolucao,
                e.DataDevolucao,
                e.Valor,
                e.Multa,
                e.Total,
                e.DataCriacao,
                e.DataAtualizacao);
        }
    }
}
