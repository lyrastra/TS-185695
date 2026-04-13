using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outsource.Incoming;

[ApiController]
[ApiVersion("1")]
[Route("private/api/v{version:apiVersion}/PaymentOrders/Incoming/RefundFromAccountablePerson/Outsource")]
[ApiExplorerSettings(IgnoreApi = true)]
public class OutsourceRefundFromAccountablePersonController : ControllerBase
{
    private readonly IRefundFromAccountablePersonOutsourceProcessor processor;

    public OutsourceRefundFromAccountablePersonController(IRefundFromAccountablePersonOutsourceProcessor processor)
    {
        this.processor = processor;
    }

    [HttpPost("Confirm")]
    [ProducesResponseType(200, Type = typeof(OutsourceConfirmResultDto))]
    public async Task<IActionResult> ConfirmAsync(ConfirmRefundFromAccountablePersonDto request)
    {
        var result = await processor.ConfirmAsync(request.ToSaveRequest());
        return new ApiDataResult(result.ToDto());
    }
}