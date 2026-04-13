using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Business.Abstractions.Operations;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/OperationsValidation")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OperationsValidationPrivateController : ControllerBase
    {
        private readonly IOperationsValidationService operationsValidationService;

        public OperationsValidationPrivateController(
            IOperationsValidationService operationsValidationService)
        {
            this.operationsValidationService = operationsValidationService;
        }

        [HttpPost("GetDocumentsStatusByQuery")] 
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(ApiDataResult))]
        public async Task<IActionResult> GetDocumentsStatusByQueryAsync(DocumentsStatusQueryDto queryDto)
        {
            if (queryDto.DocBaseIds == null)
                return NoContent();
            
            var documentsRequest = MapToDocumentsStatusRequest(queryDto);
            
            var checkDocs = await operationsValidationService
                .GetDocumentsStatusByBaseIdsAsync(documentsRequest);
            
            return new ApiDataResult(checkDocs);
        }

        private static DocumentsStatusRequest MapToDocumentsStatusRequest(DocumentsStatusQueryDto query)
        {
            return new DocumentsStatusRequest
            {
                DocBaseIds = query.DocBaseIds,
                IsAllPaid = query.IsAllPaid,
                IsPassedOutsourcingCheck = query.IsPassedOutsourcingCheck,
            };
        }
    }
}