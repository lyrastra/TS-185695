using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Providing.Api.Mappers;
using Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Providing.Api.Models;
using Moedelo.Money.Providing.Api.Models.AccPostings;
using Moedelo.Money.Providing.Api.Models.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Outgoing/PaymentToSupplier/AccPostings")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentToSupplierAccPostingsController : ControllerBase
    {
        private readonly IPaymentToSupplierAccPostingsFullGenerator generator;

        public PaymentToSupplierAccPostingsController(
            IPaymentToSupplierAccPostingsFullGenerator generator)
        {
            this.generator = generator;
        }

        /// <summary>
        /// Оплата поставщику
        /// </summary>
        [HttpPost("")]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<AccPostingsResponseDto>))]
        [ProducesResponseType(422)]
        public async Task<IActionResult> GenerateAsync(PaymentToSupplierAccPostingsGenerateRequestDto dto)
        {
            var model = PaymentToSupplierAccPostingsMapper.Map(dto);
            var postings = await generator.GenerateAsync(model).ConfigureAwait(false);
            var responseDto = AccPostingsMapper.MapPostings(postings);
            return new ApiDataResult(responseDto);
        }
    }
}
