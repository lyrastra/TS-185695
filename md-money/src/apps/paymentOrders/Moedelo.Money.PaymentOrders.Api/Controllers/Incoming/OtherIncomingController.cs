using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.PaymentOrders.Api.Controllers.Incoming
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Incoming/Other")]
    public class OtherIncomingController : ControllerBase
    {
        private readonly IPaymentOrderReader reader;
        private readonly IPaymentOrderCreator creator;
        private readonly IPaymentOrderUpdater updater;
        private readonly IPaymentOrderRemover remover;

        public OtherIncomingController(
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
            var model = await reader.GetByBaseIdAsync(documentBaseId, OperationType.PaymentOrderIncomingOther);
            if (model == null)
            {
                return NotFound();
            }
            var data = OtherIncomingMapper.Map(model);
            return new ApiDataResult(data);
        }

        [HttpPost("ByBaseIds")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetByDocumentBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var models = await reader.GetByBaseIdsAsync(documentBaseIds, OperationType.PaymentOrderIncomingOther);
            var data = models.Select(OtherIncomingMapper.Map).ToArray();
            return new ApiDataResult(data);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateAsync(OtherIncomingDto dto)
        {
            var request = OtherIncomingMapper.Map(dto);
            var response = await creator.CreateAsync(request);
            return new ApiDataResult(response) { StatusCode = 201 };
        }

        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, [FromBody] OtherIncomingDto dto)
        {
            dto.DocumentBaseId = documentBaseId;
            var request = OtherIncomingMapper.Map(dto);
            request.DocumentBaseId = documentBaseId;
            await updater.UpdateAsync(request);
            return Ok();
        }

        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
            return NoContent();
        }
    }
}
