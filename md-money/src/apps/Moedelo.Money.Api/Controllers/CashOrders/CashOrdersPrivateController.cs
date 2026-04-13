using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Business.Abstractions.CashOrders;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.CashOrders
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/CashOrders")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff)]
    public class CashOrdersPrivateController : ControllerBase
    {
        private readonly ICashOrderGetter getter;

        public CashOrdersPrivateController(
            ICashOrderGetter getter)
        {
            this.getter = getter;
        }

        /// <summary>
        /// Получение Id операции
        /// </summary>
        [HttpGet("{documentBaseId:long}/Id")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<long>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Касса" })]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetIdAsync(long documentBaseId)
        {
            var id = await getter.GetOperationIdAsync(documentBaseId);
            return new ApiDataResult(id);
        }
    }
}
