using Microsoft.AspNetCore.Mvc;
using Moedelo.Money.Numeration.Business.Abstractions.PaymentOrders;
using System.Threading.Tasks;
using Moedelo.Money.Numeration.Api.Endpoints.PaymentOrders.Dto;

namespace Moedelo.Money.Numeration.Api.Endpoints.PaymentOrders;

[ApiController]
[ApiVersion("1")]
[Route("private/api/v{version:apiVersion}/PaymentOrderNumeration")]
[ApiExplorerSettings(IgnoreApi = true)]
public class PaymentOrderNumerationPrivateController : ControllerBase
{
    private readonly INumberSetter numberSetter;

    public PaymentOrderNumerationPrivateController(INumberSetter numberSetter)
    {
        this.numberSetter = numberSetter;
    }

    /// <summary>
    /// Установить минимальную границу для вычисления следующего номера
    /// </summary>
    [HttpPost("")]
    public async Task<IActionResult> SetLastNumberAsync(PaymentOrderNumerationDto dto)
    { 
        await numberSetter.Last(dto.SettlementAccountId, dto.Year, dto.LastNumber);
        return Ok();
    }
}