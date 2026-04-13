using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.BudgetaryPayments
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/BudgetaryPaymentsTaxWidget")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BudgetaryPaymentsTaxWidgetController : ControllerBase
    {
        private readonly IBudgetaryPaymentTaxWidget budgetaryPaymentReader;

        public BudgetaryPaymentsTaxWidgetController(IBudgetaryPaymentTaxWidget budgetaryPaymentReader)
        {
            this.budgetaryPaymentReader = budgetaryPaymentReader;
        }

        [HttpPost("GetBudgetaryTaxPayments")]
        public async Task<IActionResult> GetBudgetaryNdsPaymentsAsync(BudgetaryPaymentTaxWidgetDto requestDto)
        {
            var request = BudgetaryPaymentTaxWidget.Map(requestDto);
            var response = await budgetaryPaymentReader.GetBudgetaryTaxPaymentsAsync(request).ConfigureAwait(false);
            return new ApiDataResult(response);
        }
    }
}