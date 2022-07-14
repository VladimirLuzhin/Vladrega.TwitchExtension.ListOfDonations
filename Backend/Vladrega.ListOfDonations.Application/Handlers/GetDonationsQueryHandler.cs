using Vladrega.ListOfDonations.Application.Queries;

namespace Vladrega.ListOfDonations.Application.Handlers;

/// <summary>
/// Обработчик запроса на получение списка донатов
/// </summary>
public class GetDonationsQueryHandler
{
    private readonly IDonationsRepository _donationsRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    public GetDonationsQueryHandler(IDonationsRepository donationsRepository)
    {
        _donationsRepository = donationsRepository;
    }

    /// <summary>
    /// Обработка запроса на получение списка донатеров
    /// </summary>
    /// <param name="query">Данные для выполнения запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<ChannelData> HandleAsync(GetDonationsQuery query, CancellationToken cancellationToken)
    {
        return _donationsRepository.GetChannelDataAsync(query.ChannelId, cancellationToken);
    }
}