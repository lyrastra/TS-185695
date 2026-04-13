using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.RentPayments
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/RentPayment/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PeriodsController : ControllerBase
    {
        private readonly IRentPaymentPeriodApiClient apiClient;

        public PeriodsController(IRentPaymentPeriodApiClient apiClient)
        {
            this.apiClient = apiClient;
        }


        [Route("GetByPaymentBaseIds")]
        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetByPaymentBaseIdsAsync(IReadOnlyCollection<long> ids)
        {
            // пробрасываем запрос в приватный контроллер           
            var response = await apiClient.PostAsync<IReadOnlyCollection<long>, IReadOnlyCollection<RentPaymentPeriodDto>>(ids);
            return new ApiDataResult(response);
        }
    }
}
