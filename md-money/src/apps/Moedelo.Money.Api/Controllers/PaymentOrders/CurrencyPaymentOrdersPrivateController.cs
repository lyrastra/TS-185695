using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.CurrencyPayments.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Currency;

namespace Moedelo.Money.Api.Controllers.PaymentOrders
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Currency")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff)]
    public class CurrencyPaymentOrdersPrivateController : ControllerBase
    {
        private readonly ICurrencyPaymentOrderGetter getter;

        public CurrencyPaymentOrdersPrivateController(ICurrencyPaymentOrderGetter getter)
        {
            this.getter = getter;
        }

        /// <summary>
        /// Получение валютных платежей за период
        /// </summary>
        [HttpGet("ByPeriod")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ByPeriodAsync([FromQuery]PeriodRequestDto dto)
        {
            var request = CurrencyPaymentOrderMapper.Map(dto);
            var response = await getter.ByPeriodAsync(request);
            var result = response.Select(CurrencyPaymentOrderMapper.MapToDto).ToArray();
            return new ApiDataResult(result);
        }
        
        /// <summary>
        /// Получение валютных платежей по списку baseId
        /// </summary>
        [HttpPost("ByBaseIds")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetByBaseIds(IReadOnlyCollection<long> baseIds)
        {
            var response = await getter.ByBaseIdsAsync(baseIds);
            var result = response.Select(CurrencyPaymentOrderMapper.MapToDto).ToArray();
            return new ApiDataResult(result);
        }
        
        /// <summary>
        /// Получить баланс валютных операций на дату
        /// </summary>
        [HttpGet("BalanceOnDate")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> BalanceOnDateAsync([FromQuery]DateTime date)
        {
            var response = await getter.BalanceOnDateAsync(date);
            var result = response.Select(CurrencyPaymentOrderMapper.MapToDto).ToArray();
            return new ApiDataResult(result);
        }
    }
}