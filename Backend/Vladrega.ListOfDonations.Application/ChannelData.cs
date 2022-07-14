namespace Vladrega.ListOfDonations.Application;

/// <summary>
/// Данные канала
/// </summary>
public record ChannelData
{
    /// <summary>
    /// Донаты
    /// </summary>
    public IEnumerable<Donations> Donations { get; init; }
    
    /// <summary>
    /// Тема для донатеров
    /// </summary>
    public Theme Theme { get; init; }

    /// <summary>
    /// .ctor
    /// </summary>
    public ChannelData(IEnumerable<Donations> donations, Theme theme)
    {
        Donations = donations;
        Theme = theme;
    }
}