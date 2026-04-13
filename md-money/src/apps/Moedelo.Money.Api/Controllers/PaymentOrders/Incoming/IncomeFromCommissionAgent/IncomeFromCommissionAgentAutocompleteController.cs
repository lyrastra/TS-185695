using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Autocomplete;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Incoming
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Incoming/IncomeFromCommissionAgent/Autocomplete")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class IncomeFromCommissionAgentAutocompleteController : ControllerBase
    {
        private readonly ICommissionAgentAutocompleteReader commissionAgentAutocompleteReader;

        public IncomeFromCommissionAgentAutocompleteController(
            ICommissionAgentAutocompleteReader commissionAgentAutocompleteReader)
        {
            this.commissionAgentAutocompleteReader = commissionAgentAutocompleteReader;
        }

        /// <summary>
        /// Автокомплит комиссионеров
        /// </summary>
        [HttpGet("CommissionAgent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Поступления - Поступление от комиссионера" })]
        public async Task<IActionResult> GetAutocompleteAsync([FromQuery] CommissionAgentAutocompleteRequestDto requestDto)
        {
            if (requestDto.Count > 10)
            {
                requestDto.Count = 10;
            }
            var autocomplete = await commissionAgentAutocompleteReader.GetAsync(requestDto.Date, requestDto.Query, requestDto.Count);
            var dto = autocomplete.Select(IncomeFromCommissionAgentMapper.Map).ToArray();
            return new ApiDataResult(dto);
        }
    }
}
