using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.BudgetaryPayments
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Outgoing/BudgetaryPayment")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class BudgetaryPaymentPrivateController : ControllerBase
    {
        private readonly IBudgetaryPaymentPayerKppSetter payerKppSetter;

        public BudgetaryPaymentPrivateController(
            IBudgetaryPaymentPayerKppSetter payerKppSetter)
        {
            this.payerKppSetter = payerKppSetter;
        }

        [HttpPut("{documentBaseId:long}/Payer/Kpp")]
        [ApiExplorerSettings(IgnoreApi = true)]

        public async Task<IActionResult> SetPayerKppAsync(long documentBaseId, [FromBody] string kpp)
        {
            await payerKppSetter.SetPayerKppAsync(documentBaseId, kpp);
            return Ok();
        }
    }
}