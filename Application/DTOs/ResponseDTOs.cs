using BibliotecaApi.Domain.Enums;

namespace BibliotecaApi.Application.DTOs
{
    public record LivroResponse(int Id, string Titulo, string Autor, string ISBN, bool Ativo,DateTime DataCriacao, DateTime? DataAtualizacao);

    public record UsuarioResponse(int Id, string Nome, string CPF, string Email, NivelAcesso NivelAcesso, bool Ativo, bool PossuiAtrasoAtivo, DateTime DataCriacao, DateTime? DataAtualizacao);

    public record EmprestimoResponse(
        int Id, 
        UsuarioResponse? Usuario, 
        LivroResponse? Livro, 
        DateTime DataEmprestimo, 
        DateTime DataPrevistaDevolucao, 
        DateTime? DataDevolucao, 
        decimal Valor, 
        decimal Multa, 
        decimal Total, 
        DateTime DataCriacao, 
        DateTime? DataAtualizacao);
}
