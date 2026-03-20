using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Application.UseCases.Livros;
using BibliotecaApi.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LivrosController : ControllerBase
    {
        private readonly CriarLivroUseCase _criarUseCase;
        private readonly AtualizarLivroUseCase _atualizarUseCase;
        private readonly ObterLivroUseCase _obterUseCase;
        private readonly ExcluirLivroUseCase _excluirUseCase;
        private readonly AlternarStatusLivroUseCase _alternarStatusUseCase;

        public LivrosController(
            CriarLivroUseCase criarUseCase,
            AtualizarLivroUseCase atualizarUseCase,
            ObterLivroUseCase obterUseCase,
            ExcluirLivroUseCase excluirUseCase,
            AlternarStatusLivroUseCase alternarStatusUseCase)
        {
            _criarUseCase = criarUseCase;
            _atualizarUseCase = atualizarUseCase;
            _obterUseCase = obterUseCase;
            _excluirUseCase = excluirUseCase;
            _alternarStatusUseCase = alternarStatusUseCase;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetAll()
        {
            var livros = await _obterUseCase.ObterTodosAsync();
            return Ok(ApiResponse<IEnumerable<LivroResponse>>.Success(livros));
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var livro = await _obterUseCase.ObterPorIdAsync(id);
            if (livro == null) return NotFound(ApiResponse<object>.Error("Livro não encontrado."));
            return Ok(ApiResponse<LivroResponse>.Success(livro));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("cadastrar")]
        public async Task<IActionResult> Create([FromBody] CriarLivroRequest request)
        {
            await _criarUseCase.ExecuteAsync(request);
            return Ok(ApiResponse<string>.Success("Livro criado com sucesso."));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarLivroRequest request)
        {
            request = request with { Id = id };

            await _atualizarUseCase.ExecuteAsync(request);
            return Ok(ApiResponse<string>.Success("Livro atualizado com sucesso."));
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("deletar/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _excluirUseCase.ExecuteAsync(id);
            return Ok(ApiResponse<string>.Success("Livro deletado com sucesso."));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ToggleStatus(int id, bool ativo)
        {
            await _alternarStatusUseCase.ExecuteAsync(id, ativo);
            return Ok(ApiResponse<string>.Success($"Livro {(ativo ? "ativado" : "desativado")} com sucesso."));
        }
    }
}
