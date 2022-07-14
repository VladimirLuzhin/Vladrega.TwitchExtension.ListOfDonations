using FluentResults;
using Vladrega.ListOfDonations.Application.Commands;

namespace Vladrega.ListOfDonations.Application.Handlers;

/// <summary>
/// Обработчик команды на сохранение донатов
/// </summary>
public class SaveDonationsCommandHandler
{
    private readonly DonationsParser _donationsParser;
    private readonly IDonationsRepository _donationsRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    public SaveDonationsCommandHandler(DonationsParser donationsParser, IDonationsRepository donationsRepository)
    {
        _donationsParser = donationsParser;
        _donationsRepository = donationsRepository;
    }

    /// <summary>
    /// Обработка запроса
    /// </summary>
    /// <param name="command">Данные для выполнения запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<Result> HandleAsync(SaveDonationsCommand? command, CancellationToken cancellationToken)
    {
        if (command is null)
            return Result.Fail("Переданны некорректные данные");
        
        if (command.ChannelId <= 0)
            return Result.Fail("Переданн некорректный идентификатор канала");

        var donations = _donationsParser.Parse(command.Donations);

        await _donationsRepository.SaveChannelDataAsync(command.ChannelId, command.SelectedTheme, donations, cancellationToken);
        
        return Result.Ok();
    }
}