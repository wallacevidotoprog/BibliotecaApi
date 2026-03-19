using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Application.UseCases.Usuarios;
using BibliotecaApi.Domain.Enums;
using BibliotecaApi.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly CriarUsuarioUseCase _criarUseCase;
        private readonly AtualizarUsuarioUseCase _atualizarUseCase;
        private readonly ObterUsuarioUseCase _obterUseCase;
        private readonly ExcluirUsuarioUseCase _excluirUseCase;
        private readonly AlternarStatusUsuarioUseCase _alternarStatusUseCase;

        public UsuariosController(
            CriarUsuarioUseCase criarUseCase,
            AtualizarUsuarioUseCase atualizarUseCase,
            ObterUsuarioUseCase obterUseCase,
            ExcluirUsuarioUseCase excluirUseCase,
            AlternarStatusUsuarioUseCase alternarStatusUseCase)
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
            var usuarios = await _obterUseCase.ObterTodosAsync();
            return Ok(ApiResponse<IEnumerable<Domain.Entities.UsuariosEntity>>.Success(usuarios));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _obterUseCase.ObterPorIdAsync(id);
            if (usuario == null) return NotFound(ApiResponse<object>.Error("Usuário não encontrado."));
            return Ok(ApiResponse<Domain.Entities.UsuariosEntity>.Success(usuario));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarUsuarioRequest request)
        {
            await _criarUseCase.ExecuteAsync(request);
            return Ok(ApiResponse<string>.Success("Usuário criado com sucesso."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarUsuarioRequest request)
        {
            if (id != request.Id) return BadRequest(ApiResponse<object>.Error("Id divergente."));

            await _atualizarUseCase.ExecuteAsync(request);
            return Ok(ApiResponse<string>.Success("Usuário atualizado com sucesso."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _excluirUseCase.ExecuteAsync(id);
            return Ok(ApiResponse<string>.Success("Usuário deletado com sucesso."));
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ToggleStatus(int id, bool ativo)
        {
            await _alternarStatusUseCase.ExecuteAsync(id, ativo);
            return Ok(ApiResponse<string>.Success($"Usuário {(ativo ? "ativado" : "desativado")} com sucesso."));
        }
    }
}
