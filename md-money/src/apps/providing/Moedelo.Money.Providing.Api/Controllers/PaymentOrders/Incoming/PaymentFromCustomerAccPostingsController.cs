using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Providing.Api.Mappers;
using Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Api.Models;
using Moedelo.Money.Providing.Api.Models.AccPostings;
using Moedelo.Money.Providing.Api.Models.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;

namespace Moedelo.Money.Providing.Api.Controllers.PaymentOrders.Incoming
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Incoming/PaymentFromCustomer/AccPostings")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentFromCustomerAccPostingsController : ControllerBase
    {
        private readonly IPaymentFromCustomerAccPostingsFullGenerator generator;

        public PaymentFromCustomerAccPostingsController(
            IPaymentFromCustomerAccPostingsFullGenerator generator)
        {
            this.generator = generator;
        }

        /// <summary>
        /// Оплата от покупателя
        /// </summary>
        [HttpPost("")]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<AccPostingsResponseDto>))]
        [ProducesResponseType(422)]
        public async Task<IActionResult> GenerateAsync(PaymentFromCustomerAccPostingsGenerateRequestDto dto)
        {
            var model = PaymentFromCustomerAccPostingsMapper.Map(dto);
            var postings = await generator.GenerateAsync(model).ConfigureAwait(false);
            var responseDto = AccPostingsMapper.MapPostings(postings);
            return new ApiDataResult(responseDto);
        }
    }
}
