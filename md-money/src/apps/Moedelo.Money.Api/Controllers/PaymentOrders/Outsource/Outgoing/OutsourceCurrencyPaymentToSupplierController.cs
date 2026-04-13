using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outsource.Outgoing;

[ApiController]
[ApiVersion("1")]
[Route("private/api/v{version:apiVersion}/PaymentOrders/Outgoing/CurrencyPaymentToSupplier/Outsource")]
[ApiExplorerSettings(IgnoreApi = true)]
public class OutsourceCurrencyPaymentToSupplierController : ControllerBase
{
    private readonly ICurrencyPaymentToSupplierOutsourceProcessor processor;

    public OutsourceCurrencyPaymentToSupplierController(ICurrencyPaymentToSupplierOutsourceProcessor processor)
    {
        this.processor = processor;
    }

    [HttpPost("Confirm")]
    [ProducesResponseType(200, Type = typeof(OutsourceConfirmResultDto))]
    public async Task<IActionResult> ConfirmAsync(ConfirmCurrencyPaymentToSupplierDto request)
    {
        var result = await processor.ConfirmAsync(request.ToSaveRequest());
        return new ApiDataResult(result.ToDto());
    }
}