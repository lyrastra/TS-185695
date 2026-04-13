using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.CurrencyBankFee;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Controllers.Outgoing
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Outgoing/[controller]")]
    public class CurrencyBankFeeController : ControllerBase
    {
        private readonly IPaymentOrderReader reader;
        private readonly ICurrencyBankFeeCreator creator;
        private readonly ICurrencyBankFeeUpdater updater;
        private readonly IPaymentOrderRemover remover;

        public CurrencyBankFeeController(
            IPaymentOrderReader reader,
            ICurrencyBankFeeCreator creator,
            ICurrencyBankFeeUpdater updater,
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
            var model = await reader.GetByBaseIdAsync(documentBaseId, OperationType.CurrencyBankFee).ConfigureAwait(false);
            if (model == null)
            {
                return NotFound();
            }
            var data = CurrencyBankFeeMapper.Map(model);
            return new ApiDataResult(data);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateAsync(CurrencyBankFeeDto dto)
        {
            var request = CurrencyBankFeeMapper.Map(dto);
            var response = await creator.CreateAsync(request).ConfigureAwait(false);
            return new ApiDataResult(response) { StatusCode = 201 };
        }

        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, [FromBody] CurrencyBankFeeDto dto)
        {
            dto.DocumentBaseId = documentBaseId;
            var request = CurrencyBankFeeMapper.Map(dto);
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