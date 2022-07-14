using Microsoft.AspNetCore.Mvc;
using Vladrega.ListOfDonations.Application.Commands;
using Vladrega.ListOfDonations.Application.Handlers;
using Vladrega.ListOfDonations.Application.Queries;
using Vladrega.ListOfDonations.Controllers.Dto;

namespace Vladrega.ListOfDonations.Controllers;

/// <summary>
/// Контроллер для апи сохранения, чтения, удаления донатов
/// </summary>
[Route("api/v1/donations"), ApiController]
public class DonationsController : ControllerBase
{
    private readonly SaveDonationsCommandHandler _saveDonationsCommandHandler;
    private readonly GetDonationsQueryHandler _getDonationsQueryHandler;
    private readonly DeleteDonationsCommandHandler _deleteDonationsCommandHandler;
    
    /// <summary>
    /// .ctor
    /// </summary>
    public DonationsController(SaveDonationsCommandHandler saveDonationsCommandHandler, GetDonationsQueryHandler getDonationsQueryHandler, DeleteDonationsCommandHandler deleteDonationsCommandHandler)
    {
        _saveDonationsCommandHandler = saveDonationsCommandHandler;
        _getDonationsQueryHandler = getDonationsQueryHandler;
        _deleteDonationsCommandHandler = deleteDonationsCommandHandler;
    }

    /// <summary>
    /// Получение донатов по идентификатору канала
    /// </summary>
    /// <param name="query">Данные для выполнения запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpGet]
    public async Task<IActionResult> GetDonationsAsync([FromQuery] GetDonationsQuery query, CancellationToken cancellationToken)
    {
        var channelData = await _getDonationsQueryHandler.HandleAsync(query, cancellationToken);
        return Ok(new ChannelDataDto(channelData.Theme, channelData.Donations.Select(d => new DonationsDto(d.From, d.Amount))));
    }

    /// <summary>
    /// Сохранение информации о донатерах канала
    /// </summary>
    /// <param name="command">Данные для выполнения запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPost]
    public async Task<IActionResult> SaveDonationsAsync(SaveDonationsCommand command, CancellationToken cancellationToken)
    {
        var result = await _saveDonationsCommandHandler.HandleAsync(command, cancellationToken);
        if (result.IsFailed)
            return BadRequest(string.Join(", ", result.Reasons));
        
        return Ok();
    }

    /// <summary>
    /// Удаление всей информации о донатах по идентификатору канала
    /// </summary>
    /// <param name="command">Данные для выполнения запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpDelete]
    public async Task<IActionResult> DeleteAllRows([FromQuery] DeleteDonationsCommand command, CancellationToken cancellationToken)
    {
        var result = await _deleteDonationsCommandHandler.HandleAsync(command, cancellationToken);
        if (result.IsFailed)
            return BadRequest(string.Join(", ", result.Reasons));
        
        return Ok();
    }
}