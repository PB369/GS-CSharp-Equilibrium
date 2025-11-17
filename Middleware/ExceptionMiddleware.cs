using System.Net;
using System.Text.Json;

namespace Equilibrium.Api.Middleware
{
    /// <summary>
    /// Middleware responsável por capturar exceções não tratadas,
    /// registrar o erro e retornar uma resposta JSON padronizada.
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// Delegate para chamar o próximo middleware no pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Logger utilizado para registrar exceções capturadas.
        /// </summary>
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Inicializa o middleware com o próximo delegate e o logger.
        /// </summary>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Executa o middleware, capturando e manipulando exceções não tratadas.
        /// </summary>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(new { message = "Erro interno no servidor." });
                await httpContext.Response.WriteAsync(result);
            }
        }
    }

    /// <summary>
    /// Classe de extensão para registrar o middleware global de tratamento de exceções.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Registra o middleware de exceções no pipeline da aplicação.
        /// </summary>
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
