using BibliotecaApi.Domain.Enums;

namespace BibliotecaApi.Application.DTOs
{
    public record LivroResponse(int Id, string Titulo, string Autor, string ISBN, bool Ativo, bool EmUso, DateTime DataCriacao, DateTime? DataAtualizacao);

    public record UsuarioResponse(int Id, string Nome, string CPF, string Email, string NivelAcesso, bool Ativo, bool PossuiAtrasoAtivo, DateTime DataCriacao, DateTime? DataAtualizacao);

    public record UsuarioEmprestimoResponse(int Id, string Nome);
    public record LivroEmprestimoResponse(int Id, string Titulo, string ISBN);
    public record EmprestimoResponse(
        int Id, 
        UsuarioEmprestimoResponse Usuario, 
        LivroEmprestimoResponse Livro, 
        DateTime DataEmprestimo, 
        DateTime DataPrevistaDevolucao, 
        DateTime? DataDevolucao, 
        decimal Valor, 
        decimal Multa, 
        decimal Total, 
        DateTime DataCriacao, 
        DateTime? DataAtualizacao);
}
