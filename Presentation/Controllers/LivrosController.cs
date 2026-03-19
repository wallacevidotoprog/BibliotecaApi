using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Application.UseCases.Livros;
using BibliotecaApi.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var livros = await _obterUseCase.ObterTodosAsync();
            return Ok(ApiResponse<IEnumerable<Domain.Entities.LivroEntity>>.Success(livros));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var livro = await _obterUseCase.ObterPorIdAsync(id);
            if (livro == null) return NotFound(ApiResponse<object>.Error("Livro não encontrado."));
            return Ok(ApiResponse<Domain.Entities.LivroEntity>.Success(livro));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarLivroRequest request)
        {
            await _criarUseCase.ExecuteAsync(request);
            return Ok(ApiResponse<string>.Success("Livro criado com sucesso."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarLivroRequest request)
        {
            if (id != request.Id) return BadRequest(ApiResponse<object>.Error("Id divergente."));

            await _atualizarUseCase.ExecuteAsync(request);
            return Ok(ApiResponse<string>.Success("Livro atualizado com sucesso."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _excluirUseCase.ExecuteAsync(id);
            return Ok(ApiResponse<string>.Success("Livro deletado com sucesso."));
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ToggleStatus(int id, bool ativo)
        {
            await _alternarStatusUseCase.ExecuteAsync(id, ativo);
            return Ok(ApiResponse<string>.Success($"Livro {(ativo ? "ativado" : "desativado")} com sucesso."));
        }
    }
}
