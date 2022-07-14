using Microsoft.Extensions.Caching.Memory;

namespace Vladrega.ListOfDonations.Application;

/// <summary>
/// Кэшируещая прослойка для донатов
/// </summary>
public class InMemoryDonationsProxyRepository : IDonationsRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly IDonationsRepository _originalRepository;
    
    /// <summary>
    /// .ctor
    /// </summary>
    public InMemoryDonationsProxyRepository(IMemoryCache memoryCache, IDonationsRepository originalRepository)
    {
        _memoryCache = memoryCache;
        _originalRepository = originalRepository;
    }

    /// <inheritdoc />
    public Task SaveChannelDataAsync(int channelId, Theme selectedTheme, IEnumerable<Donations> donations,
        CancellationToken cancellationToken)
    {
        _memoryCache.Remove(channelId);
        return _originalRepository.SaveChannelDataAsync(channelId, selectedTheme, donations, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ChannelData> GetChannelDataAsync(int channelId, CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue<ChannelData>(channelId, out var channelData))
            return channelData;

        channelData = await _originalRepository.GetChannelDataAsync(channelId, cancellationToken);
        _memoryCache.Set(channelId, channelData, TimeSpan.FromHours(1));
        
        return channelData;
    }

    /// <inheritdoc />
    public Task DeleteAllRowsAsync(int channelId, CancellationToken cancellationToken)
    {
        _memoryCache.Remove(channelId);
        return _originalRepository.DeleteAllRowsAsync(channelId, cancellationToken);
    }
}