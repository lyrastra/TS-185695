using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Swashbuckle.AspNetCore.Annotations;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing.BudgetaryPayments
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/Outgoing/BudgetaryPayment/Autocomplete")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class BudgetaryPaymentAutocompleteController : ControllerBase
    {
        private readonly IBudgetaryPaymentCurrencyInvoiceNdsAutocompleteReader currencyInvoiceNdsAutocompleteReader;

        public BudgetaryPaymentAutocompleteController(
            IBudgetaryPaymentCurrencyInvoiceNdsAutocompleteReader currencyInvoiceNdsAutocompleteReader)
        {
            this.currencyInvoiceNdsAutocompleteReader = currencyInvoiceNdsAutocompleteReader;
        }
        
        /// <summary>
        /// Автокомплит КБК
        /// </summary>
        [HttpPost("CurrencyInvoiceNdsPayments")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<BudgetaryKbkAutocompleteResponseDto>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания - Бюджетный платеж - Автокомплиты - Платежи по НДС (импорт, для инвойсов)" })]
        public async Task<IActionResult> CurrencyInvoiceNdsPayments(CurrencyInvoiceNdsPaymentsAutocompleteRequestDto requestDto)
        {
            var request = BudgetaryPaymentAutocompleteMapper.MapToDomain(requestDto);
            var response = await currencyInvoiceNdsAutocompleteReader.GetAsync(request);
            var responseDto = BudgetaryPaymentAutocompleteMapper.MapToDto(response);
            return new ApiDataResult(responseDto);
        }
    }
}