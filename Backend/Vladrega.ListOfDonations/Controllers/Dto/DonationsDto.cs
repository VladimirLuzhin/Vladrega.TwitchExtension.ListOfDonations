using System.Text.Json.Serialization;

namespace Vladrega.ListOfDonations.Controllers.Dto;

/// <summary>
/// ДТО для данных о донате
/// </summary>
public record DonationsDto
{
    /// <summary>
    /// От кого были донаты
    /// </summary>
    [JsonPropertyName("from")]
    public string From { get; init; }
    
    /// <summary>
    /// Сумма всех полученных донатов
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    /// <summary>
    /// .ctor
    /// </summary>
    public DonationsDto(string from, decimal amount)
    {
        From = from;
        Amount = amount;
    }
}