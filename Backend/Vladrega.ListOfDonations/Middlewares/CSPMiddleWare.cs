using System.Text;
using Microsoft.Extensions.Options;
using Vladrega.ListOfDonations.Config;

namespace Vladrega.ListOfDonations.Middlewares;

/// <summary>
/// Добавление CSP заголовка в ответ
/// </summary>
public class CSPMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _extensionId;
    
    /// <summary>
    /// .ctor
    /// </summary>
    public CSPMiddleware(IOptions<TwitchConfig> config, RequestDelegate next)
    {
        _next = next;
        _extensionId = config.Value.ExtensionId;
    }

    /// <summary>
    /// Слой обработки middleware. Заворачивает весь последующий код в пайплайне в try/catch
    /// </summary>
    /// <param name="context">Текущий контекст устрйоства</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var cspHeaderStringBuilder = new StringBuilder();
        cspHeaderStringBuilder.Append($"default-src https://vladre.ga:30500 https://{_extensionId}.ext-twitch.tv;");
        cspHeaderStringBuilder.Append($"connect-src 'self' https://{_extensionId}.ext-twitch.tv https://api.twitch.tv wss://pubsub-edge.twitch.tv https://www.google-analytics.com https://stats.g.doubleclick.net;");
        cspHeaderStringBuilder.Append($"font-src 'self' https://{_extensionId}.ext-twitch.tv https://fonts.googleapis.com https://fonts.gstatic.com;");
        cspHeaderStringBuilder.Append($"img-src 'self' https://{_extensionId}.ext-twitch.tv https://www.google-analytics.com data: blob:;");
        
        cspHeaderStringBuilder.Append($"media-src 'self' https://{_extensionId}.ext-twitch.tv data: blob:;");
        cspHeaderStringBuilder.Append($"script-src 'self' https://{_extensionId}.ext-twitch.tv https://extension-files.twitch.tv https://www.google-analytics.com;");
        cspHeaderStringBuilder.Append($"style-src 'self' 'unsafe-inline' https://{_extensionId}.ext-twitch.tv https://fonts.googleapis.com;");
        cspHeaderStringBuilder.Append("frame-ancestors https://supervisor.ext-twitch.tv https://extension-files.twitch.tv https://*.twitch.tv https://*.twitch.tech https://localhost.twitch.tv:* https://localhost.twitch.tech:* http://localhost.rig.twitch.tv:*;");
        
        context.Response.Headers.Add("Content-Security-Policy", cspHeaderStringBuilder.ToString());
        
        await _next(context);
    }
}