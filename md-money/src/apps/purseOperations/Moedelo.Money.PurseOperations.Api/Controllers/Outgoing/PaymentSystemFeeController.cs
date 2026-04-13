using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Enums;
using Moedelo.Money.PurseOperations.Api.Mappers.PurseOperations.Outgoing;
using Moedelo.Money.PurseOperations.Business.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PurseOperations.Api.Controllers.Outgoing
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Outgoing/[controller]")]
    public class PaymentSystemFeeController : ControllerBase
    {
        private readonly IPurseOperationReader reader;

        public PaymentSystemFeeController(
            IPurseOperationReader reader)
        {
            this.reader = reader;
        }

        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByDocumentBaseIdAsync(int documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId, OperationType.PurseOperationComission).ConfigureAwait(false);
            var data = PaymentSystemFeeMapper.Map(model);
            return new ApiDataResult(data);
        }
    }
}
