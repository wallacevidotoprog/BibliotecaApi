using BibliotecaApi.Domain.Enums;

namespace BibliotecaApi.Application.DTOs
{
    public record CriarLivroRequest(string Titulo, string Autor, string ISBN);
    public record AtualizarLivroRequest(int Id, string Titulo, string Autor, string ISBN);

    public record CriarUsuarioRequest(string Nome, string CPF, string Email, string Senha, NivelAcesso NivelAcesso = NivelAcesso.User);
    public record AtualizarUsuarioRequest(int Id, string Nome, string CPF, string Email, string Senha);

    public record CriarEmprestimoRequest(int IdUsuario, int IdLivro, DateTime DataPrevista);
}
