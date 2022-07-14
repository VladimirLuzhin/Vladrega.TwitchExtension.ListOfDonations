namespace Vladrega.ListOfDonations.Application;

/// <summary>
/// Репозиторий для сохранения \ получения донатов
/// </summary>
public interface IDonationsRepository
{
    /// <summary>
    /// Сохранить информацию по донатам 
    /// </summary>
    /// <param name="channelId">Идентификатор канала для получения списка донатов</param>
    /// <param name="selectedTheme">Выбранная тема</param>
    /// <param name="donations">Список донатов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task SaveChannelDataAsync(int channelId, Theme selectedTheme, IEnumerable<Donations> donations, CancellationToken cancellationToken);

    /// <summary>
    /// Получить донаты по идентификатору канала
    /// </summary>
    /// <param name="channelId">Идентификатор канала</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<ChannelData> GetChannelDataAsync(int channelId, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление всех записей о данатах по каналу
    /// </summary>
    /// <param name="channelId">Идентификатор канала</param>
    /// <param name="cancellationToken">Токен отмены операции></param>
    Task DeleteAllRowsAsync(int channelId, CancellationToken cancellationToken);
}