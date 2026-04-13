using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Api.Controllers.Outgoing
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Outgoing/[controller]")]
    public class CurrencyPaymentToSupplierController : ControllerBase
    {
        private readonly IPaymentOrderReader reader;
        private readonly IPaymentOrderCreator creator;
        private readonly IPaymentOrderUpdater updater;
        private readonly IPaymentOrderRemover remover;

        public CurrencyPaymentToSupplierController(
            IPaymentOrderReader reader,
            IPaymentOrderCreator creator,
            IPaymentOrderUpdater updater,
            IPaymentOrderRemover remover)
        {
            this.reader = reader;
            this.creator = creator;
            this.updater = updater;
            this.remover = remover;
        }

        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByDocumentBaseIdAsync(int documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId, OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier).ConfigureAwait(false);
            if (model == null)
            {
                return NotFound();
            }
            var data = CurrencyPaymentToSupplierMapper.Map(model);
            return new ApiDataResult(data);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateAsync(CurrencyPaymentToSupplierDto dto)
        {
            var request = CurrencyPaymentToSupplierMapper.Map(dto);
            var response = await creator.CreateAsync(request).ConfigureAwait(false);
            return new ApiDataResult(response) { StatusCode = 201 };
        }

        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, [FromBody] CurrencyPaymentToSupplierDto dto)
        {
            dto.DocumentBaseId = documentBaseId;
            var request = CurrencyPaymentToSupplierMapper.Map(dto);
            request.DocumentBaseId = documentBaseId;
            await updater.UpdateAsync(request).ConfigureAwait(false);
            return Ok();
        }

        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId).ConfigureAwait(false);
            return NoContent();
        }
    }
}
