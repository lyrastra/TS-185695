using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.PurseOperations.Business.Abstractions;
using Moedelo.Money.PurseOperations.Dto.PurseOperations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Moedelo.Money.PurseOperations.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PurseOperationsController : ControllerBase
    {
        private readonly IPurseOperationReader reader;

        public PurseOperationsController(
            IPurseOperationReader reader)
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
        
        /// <summary>
        /// Проверка операций по DocumentBaseIds на наличие в Кошельках
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
