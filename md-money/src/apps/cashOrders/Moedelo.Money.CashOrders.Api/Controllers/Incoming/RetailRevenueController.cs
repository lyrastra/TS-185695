using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.CashOrders.Api.Mappers.CashOrders.Incoming;
using Moedelo.Money.CashOrders.Api.Mappers.CashOrders.Outgoing;
using Moedelo.Money.CashOrders.Business.Abstractions;
using Moedelo.Money.CashOrders.Dto.CashOrders.Incoming;
using Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Api.Controllers.Incoming
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/Incoming/[controller]")]
    public class RetailRevenueController : ControllerBase
    {
        private readonly ICashOrderReader reader;
        private readonly ICashOrderUpdater updater;

        public RetailRevenueController(
            ICashOrderReader reader,
            ICashOrderUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByDocumentBaseIdAsync(int documentBaseId)
        {
            var model = await reader.GetByBaseIdAsync(documentBaseId, OperationType.CashOrderIncomingFromRetailRevenue).ConfigureAwait(false);
            var data = RetailRevenueMapper.Map(model);
            return new ApiDataResult(data);
        }

        [HttpPut("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(long documentBaseId, [FromBody] RetailRevenueDto dto)
        {
            dto.DocumentBaseId = documentBaseId;
            var request = RetailRevenueMapper.Map(dto);
            request.DocumentBaseId = documentBaseId;
            await updater.UpdateAsync(request).ConfigureAwait(false);
            return Ok();
        }
    }
}
