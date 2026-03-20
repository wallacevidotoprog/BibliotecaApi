using BibliotecaApi.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Application.DTOs
{
    public record CriarLivroRequest(
    [param: Required(ErrorMessage = "Título é obrigatório.")]
    string Titulo,

    [param: Required(ErrorMessage = "Autor é obrigatório.")]
    string Autor,

    [param: Required(ErrorMessage = "ISBN é obrigatório.")]
    string ISBN
    );
    public record AtualizarLivroRequest(int Id, string? Titulo, string? Autor, string? ISBN);

    public record CriarUsuarioRequest(
    [param: Required(ErrorMessage = "Nome é obrigatório.")]
    string Nome,

    [param: Required(ErrorMessage = "CPF é obrigatório.")]
    [param: StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve conter 11 dígitos.")]
    string CPF,

    [param: Required(ErrorMessage = "Email é obrigatório.")]
    [param: EmailAddress(ErrorMessage = "Email inválido.")]
    string Email,

    [param: Required(ErrorMessage = "Senha é obrigatória.")]
    [param: MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres.")]
    string Senha,

    NivelAcesso NivelAcesso = NivelAcesso.Usuario
    );
    public record AtualizarUsuarioRequest(int Id, string? Nome, string? CPF, string? Email, string? Senha);

    public record CriarEmprestimoRequest(
    [param: Range(1, int.MaxValue, ErrorMessage = "Id do usuário inválido.")]
    int IdUsuario,

    [param: Range(1, int.MaxValue, ErrorMessage = "Id do livro inválido.")]
    int IdLivro,

    [param: Required(ErrorMessage = "Data prevista é obrigatória.")]
    DateTime DataPrevista
    );
}
