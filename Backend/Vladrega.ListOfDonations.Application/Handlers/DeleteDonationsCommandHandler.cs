using FluentResults;
using Vladrega.ListOfDonations.Application.Commands;

namespace Vladrega.ListOfDonations.Application.Handlers;

/// <summary>
/// Обработчик команды удаления информации о донатах
/// </summary>
public class DeleteDonationsCommandHandler
{
    private readonly IDonationsRepository _donationsRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    public DeleteDonationsCommandHandler(IDonationsRepository donationsRepository)
    {
        _donationsRepository = donationsRepository;
    }

    /// <summary>
    /// Обработка команды
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<Result> HandleAsync(DeleteDonationsCommand command, CancellationToken cancellationToken)
    {
        if (command.ChannelId <= 0)
            return Result.Fail("Передан некорректный идентификатор канала");

        await _donationsRepository.DeleteAllRowsAsync(command.ChannelId, cancellationToken);
        return Result.Ok();
    }
}