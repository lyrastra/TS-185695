using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outsource;

namespace Moedelo.Money.PaymentOrders.Api.Controllers;

/// <summary>
/// Для "Аутсорс - Массовая работа с клиентами - Выписки"
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("private/api/v{version:apiVersion}/OutsourcePaymentImport")] // отсылка к вызывающему приложению
[ApiExplorerSettings(IgnoreApi = true)]
public class OutsourcePaymentImportController(
    IOutsourcePaymentImportService service,
    IOutsourceApproveService approveService
    ) : ControllerBase
{
    /// <summary>
    /// Данные для отчета по подтверждениям
    /// </summary>
    /// <remarks>
    /// При запросе отчета за месяц (максимально), требуется 400К+ записей по разным фирмам.
    /// В таком случае загрузка разбивается на части по 10К.
    /// </remarks>
    [HttpPost("ConfirmClickReport/GetByBaseIds")]
    public async Task<IActionResult> GetByBaseIdsAsync([FromBody] long[] baseIds, CancellationToken ctx)
    {
        var result = await service.GetAsync(baseIds, ctx);
        return new JsonResult(result);
    }

    /// <summary>
    /// Снять блокировку "желтая таблица" (при удалении п/п с массовой страницы)
    /// </summary>
    [HttpPost("UnsetOutsourceState")]
    [AllowAnonymous]
    public async Task<IActionResult> UnsetOutsourceState(
        [FromQuery, Required] int userId,
        [FromBody] long[] baseIds, CancellationToken ctx)
    {
        await service.UpdateOutsourceStateAsync(userId, baseIds, null, ctx);
        return new OkResult();
    }

    /// <summary>
    /// Снять блокировку "желтая таблица" и первести операцию в статус "подтврждена аутсорсером"
    /// (что не добавлять на массовую страницу операцию обработанную ML)
    /// </summary>
    [HttpPost("UnsetOutsourceStateAndApprove")]
    //[AllowAnonymous]
    public async Task<IActionResult> UnsetOutsourceStateAndApprove(
        [FromQuery, Required] int userId,
        [FromBody] long[] baseIds,
        CancellationToken ct)
    {
        await service.UpdateOutsourceStateAsync(userId, baseIds, null, ct);
        await approveService.SetIsApprovedAsync(baseIds);
        return new OkResult();
    }
}