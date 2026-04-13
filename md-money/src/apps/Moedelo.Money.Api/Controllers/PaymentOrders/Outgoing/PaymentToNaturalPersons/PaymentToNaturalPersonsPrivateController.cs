using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Outgoing/PaymentToNaturalPersons")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentToNaturalPersonsPrivateController : ControllerBase
    {
        private readonly IPaymentToNaturalPersonsReader reader;

        public PaymentToNaturalPersonsPrivateController(
            IPaymentToNaturalPersonsReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Получение операций по списку идентификаторов
        /// </summary>
        [HttpPost("GetByBaseIds")]
        public async Task<IActionResult> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var models = await reader.GetByBaseIdsAsync(documentBaseIds);
            var dto = models.Select(PaymentToNaturalPersonsMapper.Map).ToArray();
            return new ApiDataResult(dto);
        }
    }
}
