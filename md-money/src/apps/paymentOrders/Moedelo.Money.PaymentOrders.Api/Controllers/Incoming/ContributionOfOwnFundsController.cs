using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Api.Controllers.Incoming
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Incoming/[controller]")]
    public class ContributionOfOwnFundsController : ControllerBase
    {
        private readonly IPaymentOrderReader reader;
        private readonly IPaymentOrderCreator creator;
        private readonly IPaymentOrderUpdater updater;
        private readonly IPaymentOrderRemover remover;

        public ContributionOfOwnFundsController(
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
            var model = await reader.GetByBaseIdAsync(documentBaseId, OperationType.PaymentOrderIncomingContributionOfOwnFunds).ConfigureAwait(false);
            var data = ContributionOfOwnFundsMapper.Map(model);
            return new ApiDataResult(data);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateAsync(ContributionOfOwnFundsDto dto)
        {
            var request = ContributionOfOwnFundsMapper.Map(dto);
            var response = await creator.CreateAsync(request).ConfigureAwait(false);
            return new ApiDataResult(response) { StatusCode = 201 };
        }

        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, [FromBody] ContributionOfOwnFundsDto dto)
        {
            dto.DocumentBaseId = documentBaseId;
            var request = ContributionOfOwnFundsMapper.Map(dto);
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
