using BibliotecaApi.Domain.Exceptions;
using BibliotecaApi.Infrastructure.Common;
using System.Net;
using System.Text.Json;

namespace BibliotecaApi.Infrastructure.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            ApiResponse<object> response;

            switch (exception)
            {
                case DomainException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ApiResponse<object>.Error(exception.Message);
                    break;

                case JsonException ex:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    var mensagem = ex.Message.Contains("trailing comma")
                        ? "JSON inválido. Remova a vírgula extra no final."
                        : "JSON inválido.";

                    response = ApiResponse<object>.Error(mensagem);
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = ApiResponse<object>.Error("Erro interno no servidor.");
                    break;
            }

            var json = JsonSerializer.Serialize(response, options);
            return context.Response.WriteAsync(json);
        }
    }
}
