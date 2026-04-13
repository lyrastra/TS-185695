using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Api.Controllers.Incoming
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Incoming/[controller]")]
    public class IncomeFromCommissionAgentController : ControllerBase
    {
        private readonly IPaymentOrderReader reader;
        private readonly IPaymentOrderCreator creator;
        private readonly IPaymentOrderUpdater updater;
        private readonly IPaymentOrderRemover remover;

        public IncomeFromCommissionAgentController(
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
            var model = await reader.GetByBaseIdAsync(documentBaseId, OperationType.PaymentOrderIncomingIncomeFromCommissionAgent);
            var data = IncomeFromCommissionAgentMapper.Map(model);
            return new ApiDataResult(data);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateAsync(IncomeFromCommissionAgentDto dto)
        {
            var request = IncomeFromCommissionAgentMapper.Map(dto);
            var response = await creator.CreateAsync(request);
            return new ApiDataResult(response) { StatusCode = 201 };
        }

        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, [FromBody] IncomeFromCommissionAgentDto dto)
        {
            dto.DocumentBaseId = documentBaseId;
            var request = IncomeFromCommissionAgentMapper.Map(dto);
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
