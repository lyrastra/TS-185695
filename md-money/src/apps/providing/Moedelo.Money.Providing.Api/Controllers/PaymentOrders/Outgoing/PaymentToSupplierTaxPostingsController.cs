using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Providing.Api.Mappers;
using Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Providing.Api.Models;
using Moedelo.Money.Providing.Api.Models.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Providing.Api.Models.TaxPostings;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Api.Controllers.PaymentOrders.Outgoing
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/PaymentOrders/Outgoing/PaymentToSupplier/TaxPostings")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentToSupplierTaxPostingsController : ControllerBase
    {
        private readonly IPaymentToSupplierTaxPostingsGenerator generator;

        public PaymentToSupplierTaxPostingsController(
            IPaymentToSupplierTaxPostingsGenerator generator)
        {
            this.generator = generator;
        }

        /// <summary>
        /// Оплата поставщику
        /// </summary>
        [HttpPost("")]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<TaxPostingsResponseDto>))]
        [ProducesResponseType(422)]
        public async Task<IActionResult> GenerateAsync(PaymentToSupplierTaxPostingsGenerateRequestDto dto)
        {
            var model = PaymentToSupplierTaxPostingsMapper.Map(dto);
            var postings = await generator.GenerateAsync(model).ConfigureAwait(false);
            var responseDto = TaxPostingsMapper.MapPostings(postings);
            return new ApiDataResult(responseDto);
        }
    }
}
