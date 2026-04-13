using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Api.Controllers.Outgoing
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Outgoing/[controller]")]
    public class PaymentToNaturalPersonsController : ControllerBase
    {
        private readonly IPaymentToNaturalPersonsReader reader;
        private readonly IPaymentToNaturalPersonsCreator creator;
        private readonly IPaymentToNaturalPersonsUpdater updater;
        private readonly IPaymentToNaturalPersonsRemover remover;

        public PaymentToNaturalPersonsController(
            IPaymentToNaturalPersonsReader reader,
            IPaymentToNaturalPersonsCreator creator,
            IPaymentToNaturalPersonsUpdater updater,
            IPaymentToNaturalPersonsRemover remover)
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
            var model = await reader.GetByBaseIdAsync(documentBaseId);
            if (model == null)
            {
                return NotFound();
            }
            var data = PaymentToNaturalPersonsMapper.Map(model);
            return new ApiDataResult(data);
        }

        [HttpPost("ByBaseIds")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetByDocumentBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var models = await reader.GetByBaseIdsAsync(documentBaseIds);
            var data = models.Select(PaymentToNaturalPersonsMapper.Map).ToArray();
            return new ApiDataResult(data);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateAsync(PaymentToNaturalPersonsDto dto)
        {
            var request = PaymentToNaturalPersonsMapper.Map(dto);
            var response = await creator.CreateAsync(request);
            return new ApiDataResult(response) { StatusCode = 201 };
        }

        [HttpPost("WithMissingEmployee")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateWithMissingEmployeeAsync(PaymentToNaturalPersonsWithMissingEmployeeDto dto)
        {
            var request = PaymentToNaturalPersonsMapper.Map(dto);
            var response = await creator.CreateAsync(request);
            return new ApiDataResult(response) { StatusCode = 201 };
        }

        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, [FromBody] PaymentToNaturalPersonsDto dto)
        {
            dto.DocumentBaseId = documentBaseId;
            var request = PaymentToNaturalPersonsMapper.Map(dto);
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
