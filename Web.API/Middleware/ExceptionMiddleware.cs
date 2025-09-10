using Application.Excepciones;
using Application.Responses;
using System.Net;
using System.Text.Json;

namespace Web.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción interceptada");

                context.Response.ContentType = "application/json";
                var response = ManejarExcepcion(ex, out int statusCode);
                context.Response.StatusCode = statusCode;

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

        private static object ManejarExcepcion(Exception ex, out int statusCode)
        {
            statusCode = (int)HttpStatusCode.InternalServerError;

            return ex switch
            {
                NotFoundException nf => Set(HttpStatusCode.NotFound, ApiResponse<string>.Fallo(nf.Message), statusCode),
                BadRequestException br => Set(HttpStatusCode.BadRequest, ApiResponse<string>.Fallo(br.Message), statusCode),

                FluentValidation.ValidationException ve => Set(HttpStatusCode.BadRequest,
                    ApiResponse<IEnumerable<string>>.Fallo(
                        ve.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                    ), statusCode),

                _ => Set(HttpStatusCode.InternalServerError,
                    ApiResponse<string>.Fallo("Ocurrió un error inesperado."), statusCode)
            };

            static object Set(HttpStatusCode code, object respuesta, int statusCode)
            {
                statusCode = (int)code;
                return respuesta;
            }
        }
    }
}
