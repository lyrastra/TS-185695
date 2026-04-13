using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Numeration.Business.Abstractions.PaymentOrders;

namespace Moedelo.Money.Numeration.Api.Endpoints.PaymentOrders;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PaymentOrderNumerationController : ControllerBase
{
    private readonly INumberGetter numberGetter;

    public PaymentOrderNumerationController(
        INumberGetter numberGetter)
    {
        this.numberGetter = numberGetter;
    }

    /// <summary>
    /// Получение следующего доступнного номера для платёжного поручения
    /// </summary>
    [HttpGet("NextNumber")]
    public async Task<IActionResult> NextNumberAsync(int settlementAccountId, int year)
    {
        var response = await numberGetter.LastAndNext(settlementAccountId, year, 1);
        return new ApiDataResult(response.Item2.FirstOrDefault());
    }

    /// <summary>
    /// Получение следующих доступных номеров
    /// </summary>
    [HttpGet("NextNumbers")]
    public async Task<IActionResult> NextNumbersAsync(int settlementAccountId, int year, int? count)
    {
        var response = await numberGetter.LastAndNext(settlementAccountId, year, count);
        return new ApiDataResult(response.Item2);
    }
}