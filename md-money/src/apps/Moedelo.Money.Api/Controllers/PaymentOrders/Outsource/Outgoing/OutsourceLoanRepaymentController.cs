using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outsource.Outgoing;

[ApiController]
[ApiVersion("1")]
[Route("private/api/v{version:apiVersion}/PaymentOrders/Outgoing/LoanRepayment/Outsource")]
[ApiExplorerSettings(IgnoreApi = true)]
public class OutsourceLoanRepaymentController : ControllerBase
{
    private readonly ILoanRepaymentOutsourceProcessor processor;

    public OutsourceLoanRepaymentController(ILoanRepaymentOutsourceProcessor processor)
    {
        this.processor = processor;
    }

    [HttpPost("Confirm")]
    [ProducesResponseType(200, Type = typeof(OutsourceConfirmResultDto))]
    public async Task<IActionResult> ConfirmAsync(ConfirmLoanRepaymentDto request)
    {
        var result = await processor.ConfirmAsync(request.ToSaveRequest());
        return new ApiDataResult(result.ToDto());
    }
}