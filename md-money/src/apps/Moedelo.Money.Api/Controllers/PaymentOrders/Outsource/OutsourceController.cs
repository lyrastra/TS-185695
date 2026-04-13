using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outsource
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/PaymentOrders/[controller]")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class OutsourceController : ControllerBase
    {
        private readonly IOutsourceApproveService service;

        public OutsourceController(IOutsourceApproveService service)
        {
            this.service = service;
        }

        [HttpGet("Approve/InitialDate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetInitialDateAsync()
        {
            var startDate = await service.GetInitialDateAsync();

            return new ApiDataResult(startDate);
        }

        [HttpGet("Approve/{documentBaseId:long}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetIsApprovedAsync(long documentBaseId)
        {
            var isApproved = await service.GetIsApprovedAsync(documentBaseId);

            return new ApiDataResult(isApproved);
        }

        [HttpPost("Approve/GetByIds")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> BatchGetIsApprovedAsync([FromBody] IReadOnlyCollection<long> documentBaseId)
        {
            var result = await service.GetIsApprovedAsync(documentBaseId);
            var response = OutsourceMapper.Map(result);

            return new ApiDataResult(response);
        }

        [HttpPut("Approve/{documentBaseId:long}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ApproveAsync(long documentBaseId, bool isApproved)
        {
            await service.SetIsApprovedAsync(documentBaseId, isApproved);

            return Ok();
        }

        [HttpPost("Approve")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> BatchApproveAsync([FromBody] IReadOnlyCollection<long> documentBaseIds)
        {
            await service.SetIsApprovedAsync(documentBaseIds);

            return Ok();
        }
    }
}
