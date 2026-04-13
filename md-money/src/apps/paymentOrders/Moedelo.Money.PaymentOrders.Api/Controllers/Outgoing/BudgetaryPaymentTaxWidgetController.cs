using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using System.Threading.Tasks;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Api.Controllers.Outgoing
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Outgoing/[controller]")]
    public class BudgetaryPaymentTaxWidgetController : ControllerBase
    {
        private readonly IPaymentOrderTaxWidgetReader reader;

        public BudgetaryPaymentTaxWidgetController(IPaymentOrderTaxWidgetReader reader)
        {
            this.reader = reader;
        }

        [HttpPost("GetBudgetaryTaxPayments")]
        public async Task<IActionResult> GetBudgetaryNdsPaymentsAsync(BudgetaryPaymentTaxWidgetRequest request)
        {
            var payments = await reader.GetBudgetaryTaxPaymentsAsync(request).ConfigureAwait(false);
            return new ApiDataResult(payments);
        }
    }
}
