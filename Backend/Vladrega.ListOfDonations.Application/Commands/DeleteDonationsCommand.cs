using System.Text.Json.Serialization;

namespace Vladrega.ListOfDonations.Application.Commands;

/// <summary>
/// Команда на удаление информации о всех донатаха канала
/// </summary>
public record DeleteDonationsCommand
{
    /// <summary>
    /// Идентификатор канала
    /// </summary>
    [JsonPropertyName("channelId")]
    public int ChannelId { get; init; }
}