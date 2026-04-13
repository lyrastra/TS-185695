using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Business.Abstractions.UnifiedBudgetaryPayments;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DataController : ControllerBase
    {
        private readonly IUnifiedBudgetaryPaymentsLaunchService enpLaunchService;

        public DataController(
            IUnifiedBudgetaryPaymentsLaunchService enpLaunchService)
        {
            this.enpLaunchService = enpLaunchService;
        }

        [HttpGet("UnifiedBudgetaryPayments/StartDate")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Tags = new[] { "Деньги" })]
        public async Task<IActionResult> GetStartDateAsync()
        {
            var enpStartDate = await enpLaunchService.GetEnpStartDateAsync();
            return new ApiDataResult(new { enpStartDate });
        }
    }
}
