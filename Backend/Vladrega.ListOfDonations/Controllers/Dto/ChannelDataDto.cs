using System.Text.Json.Serialization;
using Vladrega.ListOfDonations.Application;

namespace Vladrega.ListOfDonations.Controllers.Dto;

/// <summary>
/// Список суммы всех донатов от разных донатеров
/// </summary>
public record ChannelDataDto
{
    /// <summary>
    /// Выбранная тема
    /// </summary>
    [JsonPropertyName("theme")]
    public Theme SelectedTheme { get; }
    
    /// <summary>
    /// Донаты
    /// </summary>
    [JsonPropertyName("donators")]
    public IEnumerable<DonationsDto> Donators { get; }

    /// <summary>
    /// .ctor
    /// </summary>
    public ChannelDataDto(Theme theme, IEnumerable<DonationsDto> donators)
    {
        SelectedTheme = theme;
        Donators = donators;
    }
}