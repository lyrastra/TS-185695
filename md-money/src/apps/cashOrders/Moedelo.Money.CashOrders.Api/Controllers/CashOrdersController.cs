using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.CashOrders.Business.Abstractions;
using Moedelo.Money.CashOrders.Dto.CashOrders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Moedelo.Money.CashOrders.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CashOrdersController : ControllerBase
    {
        private readonly ICashOrderReader reader;

        public CashOrdersController(
            ICashOrderReader reader)
        {
            this.reader = reader;
        }

        [HttpGet("{documentBaseId:long}/OperationType")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOperationTypeAsync(long documentBaseId)
        {
            var operationType = await reader.GetOperationTypeAsync(documentBaseId).ConfigureAwait(false);
            return new ApiDataResult(new OperationTypeDto { OperationType = operationType });
        }

        [HttpPost("OperationType")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetOperationTypesAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var operationTypes = await reader.GetOperationTypeByIdsAsync(documentBaseIds);
            return new ApiDataResult(operationTypes.Select(x => new OperationTypeDto { DocumentBaseId = x.DocumentBaseId, OperationType = x.OperationType }));
        }

        [HttpGet("{documentBaseId:long}/Id")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOperationIdAsync(long documentBaseId)
        {
            var operationId = await reader.GetOperationIdAsync(documentBaseId);
            return new ApiDataResult(new OperationIdDto { OperationId = operationId });
        }
        
        /// <summary>
        /// Проверка операций по DocumentBaseIds на наличие в Кассе
        /// </summary>
        [HttpPost("GetDocumentsStatusByBaseIds")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetDocumentsStatusByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var docsStatus = await reader.GetDocumentsStatusByBaseIdsAsync(documentBaseIds);
            return new ApiDataResult(docsStatus);
        }
    }
}
