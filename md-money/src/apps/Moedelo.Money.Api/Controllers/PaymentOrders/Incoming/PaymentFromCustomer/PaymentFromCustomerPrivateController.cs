using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Incoming.PaymentFromCustomer
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Incoming/PaymentFromCustomer")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentFromCustomerPrivateController : ControllerBase
    {
        private readonly IPaymentFromCustomerReader reader;

        public PaymentFromCustomerPrivateController(
            IPaymentFromCustomerReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Получение списка операции
        /// </summary>
        [HttpPost("GetByBaseIds")]
        public async Task<IActionResult> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var models = await reader.GetByBaseIdsAsync(documentBaseIds);
            var dtos = models
                .Select(PaymentFromCustomerMapper.Map)
                .ToArray();

            return new ApiDataResult(dtos);
        }
    }
}
