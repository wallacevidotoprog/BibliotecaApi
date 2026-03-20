using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Application.DTOs
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; } = string.Empty;
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NivelAcesso { get; set; } = string.Empty;
    }
}
