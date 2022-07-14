using System.Text.Json.Serialization;

namespace Vladrega.ListOfDonations.Application.Commands;

/// <summary>
/// Команда на сохранение списка топ-донатеров
/// </summary>
public record SaveDonationsCommand
{
    /// <summary>
    /// Идентификатор канала
    /// </summary>
    [JsonPropertyName("channelId")]
    public int ChannelId { get; init; }
    
    /// <summary>
    /// Выбранная тема
    /// </summary>
    [JsonPropertyName("theme")]
    public Theme SelectedTheme { get; init; } 
    
    /// <summary>
    /// Список донатов в текстовом, неформатированном виде
    /// </summary>
    [JsonPropertyName("donations")]
    public string Donations { get; init; }
}