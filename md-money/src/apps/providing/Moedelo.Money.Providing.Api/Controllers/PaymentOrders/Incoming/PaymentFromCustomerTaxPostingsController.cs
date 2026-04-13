using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Providing.Api.Mappers;
using Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Api.Models;
using Moedelo.Money.Providing.Api.Models.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Api.Models.TaxPostings;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;

namespace Moedelo.Money.Providing.Api.Controllers.PaymentOrders.Incoming
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Incoming/PaymentFromCustomer/TaxPostings")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentFromCustomerTaxPostingsController : ControllerBase
    {
        private readonly IPaymentFromCustomerTaxPostingsGenerator generator;

        public PaymentFromCustomerTaxPostingsController(
            IPaymentFromCustomerTaxPostingsGenerator generator)
        {
            this.generator = generator;
        }

        /// <summary>
        /// Оплата от покупателя
        /// </summary>
        [HttpPost("")]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<TaxPostingsResponseDto>))]
        [ProducesResponseType(422)]
        public async Task<IActionResult> GenerateAsync(PaymentFromCustomerTaxPostingsGenerateRequestDto dto)
        {
            var model = PaymentFromCustomerTaxPostingsMapper.Map(dto);
            var postings = await generator.GenerateAsync(model).ConfigureAwait(false);
            var responseDto = TaxPostingsMapper.MapPostings(postings);
            return new ApiDataResult(responseDto);
        }
    }
}
