using System;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Business.Abstractions.Registry;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/RegistryTaxWidgets")]
    public class RegistryTaxWidgetsPrivateController : ControllerBase
    {
        private readonly IRegistryReader registryService;

        public RegistryTaxWidgetsPrivateController(IRegistryReader registryService)
        {
            this.registryService = registryService;
        }

        /// <summary>
        /// Возвращает объединение исходящих платежей по кассе и рассчетным счетам для Виждетов НДС и Налога на прибыль
        /// </summary>
        [HttpGet("GetOutgoingPaymentsForTaxWidgets")]
        [ProducesResponseType(200, Type = typeof(ApiPageResponseDto))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetOutgoingPaymentsForTaxWidgetsAsync(DateTime startDate, DateTime endDate)
        {
            var response = await registryService.GetOutgoingPaymentsForTaxWidgetsAsync(startDate, endDate).ConfigureAwait(false);
            var operations = response.Select(RegistryMapper.MapOperationToDto).ToArray();
            
            return new ApiDataResult(operations);
        }
    }
}
