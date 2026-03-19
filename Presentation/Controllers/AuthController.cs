using BibliotecaApi.Application.DTOs;
using BibliotecaApi.Application.UseCases.Usuarios;
using BibliotecaApi.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;

        public AuthController(LoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = await _loginUseCase.Executar(request);
                return Ok(ApiResponse<LoginResponseDTO>.Success(response));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<LoginResponseDTO>.Error(ex.Message));
            }
        }
    }
}
