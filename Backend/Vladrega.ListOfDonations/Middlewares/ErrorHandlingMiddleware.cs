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

    /// <summary>
    /// .ctor
    /// </summary>
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
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
            context.Response.ContentType = MediaTypeNames.Text.Plain;
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync($"При выполнении запроса произошла ошибка: {e.Message}");
        }
    }
}