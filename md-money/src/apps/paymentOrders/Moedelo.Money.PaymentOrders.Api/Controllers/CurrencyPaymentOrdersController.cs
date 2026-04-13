using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Currency;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;

namespace Moedelo.Money.PaymentOrders.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Currency")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CurrencyPaymentOrdersController : ControllerBase
    {
        private readonly ICurrencyPaymentOrderReader reader;

        public CurrencyPaymentOrdersController(ICurrencyPaymentOrderReader reader)
        {
            this.reader = reader;
        }

        [HttpGet("ByPeriod")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ByPeriodAsync([FromQuery]PeriodRequestDto dto)
        {
            var request = CurrencyPaymentOrdersMapper.Map(dto);
            var paymentOrders = await reader.ByPeriodAsync(request);
            var dtos = paymentOrders.Select(CurrencyPaymentOrdersMapper.MapToDto).ToArray();
            return new ApiDataResult(dtos);
        }
        
        [HttpPost("ByBaseIds")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ByBaseIds(IReadOnlyCollection<long> baseIds)
        {
            var paymentOrders = await reader.ByBaseIdsAsync(baseIds);
            var dtos = paymentOrders.Select(CurrencyPaymentOrdersMapper.MapToDto).ToArray();
            return new ApiDataResult(dtos);
        }
        
        [HttpGet("BalanceOnDate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> BalanceOnDateAsync([FromQuery]DateTime date)
        {
            var balances = await reader.BalanceOnDateAsync(date);
            var dtos = balances.Select(CurrencyPaymentOrdersMapper.MapToDto).ToArray();
            return new ApiDataResult(dtos);
        }
    }
}