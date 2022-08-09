using System.Net;
using System.Net.Mime;
using System.Text;

namespace Vladrega.ListOfDonations.Middlewares;

/// <summary>
/// Глобальный обрбаотчик ошибок
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    /// <summary>
    /// .ctor
    /// </summary>
    public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
    }

    /// <summary>
    /// Слой обработки middleware. Заворачивает весь последующий код в пайплайне в try/catch
    /// </summary>
    /// <param name="context">Текущий контекст устрйоства</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "При выполнении запроса произошла ошибка");
            
            context.Response.ContentType = MediaTypeNames.Text.Plain;
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync($"При выполнении запроса произошла ошибка: {e.Message}");
        }
    }
}