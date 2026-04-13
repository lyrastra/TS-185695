using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Registry.Api.Mappers;
using Moedelo.Money.Registry.Business.Abstractions;
using Moedelo.Money.Registry.Dto;

namespace Moedelo.Money.Registry.Api.Controller
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RegistryController : ControllerBase
    {
        private readonly IRegistryReader reader;

        public RegistryController(
            IRegistryReader reader)
        {
            this.reader = reader;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PostAsync(RegistryQueryDto request)
        {
            var query = RegistryMapper.MapToDomain(request);
            var response = await reader.GetAsync(query);
            return new ApiPageResult(
                RegistryMapper.MapToResponse(response.Operations),
                response.Offset,
                response.Limit,
                response.TotalCount);
        }

        [HttpPost("GetSelfCostPayments")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PostAsync(SelfCostPaymentRequestDto request)
        {
            var query = RegistryMapper.MapToDomain(request);
            var result = await reader.GetSelfCostPaymentsAsync(query);
            var response = RegistryMapper.MapToResponse(result);
            return new ApiDataResult(response);
        }

        [HttpGet("GetOutgoingPaymentsForTaxWidgets")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetOutgoingPaymentsForTaxWidgetsAsync(DateTime startDate, DateTime endDate)
        {
            var response = await reader.GetOutgoingPaymentsForTaxWidgetsAsync(startDate, endDate);
            return new ApiDataResult(response);
        }
    }
}
