using System.Text.Json.Serialization;

namespace Vladrega.ListOfDonations.Application.Queries;

/// <summary>
/// Запрос на получение списка всех донатов по каналу
/// </summary>
public record GetDonationsQuery
{
    /// <summary>
    /// Идентификатор канала
    /// </summary>
    [JsonPropertyName("channelId")]
    public int ChannelId { get; init; }
}