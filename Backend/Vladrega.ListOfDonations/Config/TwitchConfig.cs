namespace Vladrega.ListOfDonations.Config;

/// <summary>
/// Конфигурация для Twitch
/// </summary>
public record TwitchConfig
{
    /// <summary>
    /// Идентификатор расширения
    /// </summary>
    public string ExtensionId { get; init; }
}