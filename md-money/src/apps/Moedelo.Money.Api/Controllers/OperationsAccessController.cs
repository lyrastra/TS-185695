using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;
using Moedelo.Money.Business.Abstractions.Operations;
using Moedelo.Money.Domain;
using Swashbuckle.AspNetCore.Annotations;

namespace Moedelo.Money.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/operations/access")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OperationsAccessController : ControllerBase
    {
        private readonly IOperationsAccessReader operationsAccessReader;

        public OperationsAccessController(IOperationsAccessReader operationsAccessReader)
        {
            this.operationsAccessReader = operationsAccessReader;
        }

        /// <summary>
        /// Доступ к операциям
        /// </summary>
        [HttpGet("")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Tags = new[] { "Деньги - Доступ к операциям" })]
        public async Task<IActionResult> Access()
        {
            var result = await operationsAccessReader.GetAsync();
            
            return new ApiDataResult(Map(result));
        }

        private OperationsAccessDto Map(OperationsAccessModel model)
        {
            return new OperationsAccessDto
            {
                CanEditCurrencyOperations = model.CanEditCurrencyOperations
            };
        }
    }
}