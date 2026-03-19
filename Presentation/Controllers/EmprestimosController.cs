using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Application.UseCases.Emprestimos;
using BibliotecaApi.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimosController : ControllerBase
    {
        private readonly CriarEmprestimoUseCase _criarUseCase;
        private readonly RegistrarDevolucaoUseCase _devolucaoUseCase;
        private readonly ObterEmprestimoUseCase _obterUseCase;
        private readonly ExcluirEmprestimoUseCase _excluirUseCase;

        public EmprestimosController(
            CriarEmprestimoUseCase criarUseCase,
            RegistrarDevolucaoUseCase devolucaoUseCase,
            ObterEmprestimoUseCase obterUseCase,
            ExcluirEmprestimoUseCase excluirUseCase)
        {
            _criarUseCase = criarUseCase;
            _devolucaoUseCase = devolucaoUseCase;
            _obterUseCase = obterUseCase;
            _excluirUseCase = excluirUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var emprestimos = await _obterUseCase.ObterTodosAsync();
            return Ok(ApiResponse<IEnumerable<Domain.Entities.EmprestimoEntity>>.Success(emprestimos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emprestimo = await _obterUseCase.ObterPorIdAsync(id);
            if (emprestimo == null) return NotFound(ApiResponse<object>.Error("Empréstimo não encontrado."));
            return Ok(ApiResponse<Domain.Entities.EmprestimoEntity>.Success(emprestimo));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarEmprestimoRequest request)
        {
            await _criarUseCase.ExecuteAsync(request);
            return Ok(ApiResponse<string>.Success("Empréstimo registrado com sucesso."));
        }

        [HttpPatch("{id}/devolucao")]
        public async Task<IActionResult> RegistrarDevolucao(int id)
        {
            await _devolucaoUseCase.ExecuteAsync(id);
            return Ok(ApiResponse<string>.Success("Devolução registrada com sucesso."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _excluirUseCase.ExecuteAsync(id);
            return Ok(ApiResponse<string>.Success("Empréstimo deletado com sucesso."));
        }
    }
}
