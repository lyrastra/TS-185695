using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Enums;
using Moedelo.Money.PurseOperations.Api.Mappers.PurseOperations.Incoming;
using Moedelo.Money.PurseOperations.Business.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PurseOperations.Api.Controllers.Incoming
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Outgoing/[controller]")]
    public class PaymentFromCustomerController : ControllerBase
    {
        private readonly IPurseOperationReader reader;

        public PaymentFromCustomerController(
            IPurseOperationReader reader)
        {
            this.reader = reader;
        }

        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByDocumentBaseIdAsync(int documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId, OperationType.PurseOperationIncomingPaymentFromCustomer);
            var data = PaymentFromCustomerMapper.Map(model);
            return new ApiDataResult(data);
        }
    }
}
