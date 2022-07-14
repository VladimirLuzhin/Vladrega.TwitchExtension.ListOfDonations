namespace Vladrega.ListOfDonations.Application;

public record ChannelData
{
    public IEnumerable<Donations> Donations { get; init; }
    
    public Theme Theme { get; init; }

    public ChannelData(IEnumerable<Donations> donations, Theme theme)
    {
        Donations = donations;
        Theme = theme;
    }
}