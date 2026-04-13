using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.RentPayment;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.RentPayments
{
    [ApiController]
    [ApiVersion("1")]     
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Outgoing/RentPayment/[controller]")]
    public class PeriodsController : ControllerBase
    {
        private readonly IRentPaymentPeriodReader reader;

        public PeriodsController(IRentPaymentPeriodReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Получение списка периодов
        /// </summary>
        [Route("GetByPaymentBaseIds")]
        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetByPaymentBaseIdsAsync([FromBody] IReadOnlyCollection<long> ids)
        {
            var model = await reader.GetByPaymentBaseIdsAsync(ids);
            var dto = model.Select(RentPaymentMapper.MapRentPaymentPeriod).ToArray();
            return new ApiDataResult(dto);
        }
    }
}
