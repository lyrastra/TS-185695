using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Abstractions.Registry;

namespace Moedelo.Money.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/SelfCostPayments")]
    public class SelfCostPaymentsPrivateController : ControllerBase
    {
        private readonly IRegistryReader registryService;
        private readonly IBudgetaryPaymentTaxSelfCostReader budgetaryPaymentTaxSelfCostReader;

        public SelfCostPaymentsPrivateController(
            IRegistryReader registryService,
            IBudgetaryPaymentTaxSelfCostReader budgetaryPaymentTaxSelfCostReader)
        {
            this.registryService = registryService;
            this.budgetaryPaymentTaxSelfCostReader = budgetaryPaymentTaxSelfCostReader;
        }

        /// <summary>
        /// Возвращает платежи по кассе и рассчетным счетам для расчета себестоимости
        /// </summary>
        [HttpPost("SettlementAccountPayments")]
        [ProducesResponseType(200, Type = typeof(ApiDataResult))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetSettlementAccountPaymentsAsync(SelfCostPaymentRequestDto requestDto)
        {
            var request = SelfCostPaymentsMapper.MapToDomain(requestDto);
            var response = await registryService.GetSelfCostSettlementAccountPaymentsAsync(request);
            var operations = response.Select(SelfCostPaymentsMapper.MapToDto).ToArray();

            return new ApiDataResult(operations);
        }

        /// <summary>
        /// Возвращает платежи по кассе и рассчетным счетам для расчета себестоимости
        /// </summary>
        [HttpPost("CashPayments")]
        [ProducesResponseType(200, Type = typeof(ApiDataResult))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetCashPaymentsAsync(SelfCostPaymentRequestDto requestDto)
        {
            var request = SelfCostPaymentsMapper.MapToDomain(requestDto);
            var response = await registryService.GetSelfCostCashPaymentsAsync(request);
            var operations = response.Select(SelfCostPaymentsMapper.MapToDto).ToArray();

            return new ApiDataResult(operations);
        }

        /// <summary>
        /// Возвращает бюджетные платежи по НДС (валюта)
        /// </summary>
        [HttpPost("CurrencyNdsPayments")]
        [ProducesResponseType(200, Type = typeof(ApiDataResult))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetCurrencyNdsPaymentsAsync(SelfCostPaymentRequestDto requestDto)
        {
            var request = SelfCostPaymentsMapper.MapToDomain(requestDto);
            var response = await budgetaryPaymentTaxSelfCostReader.GetSelfCostPaymentsAsync(request);
            var operations = response.Select(SelfCostPaymentsMapper.MapToDto).ToArray();

            return new ApiDataResult(operations);
        }
    }
}