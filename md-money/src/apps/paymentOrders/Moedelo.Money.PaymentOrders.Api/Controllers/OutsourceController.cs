using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outsource;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outsource;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("private/api/v{version:apiVersion}/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OutsourceController : ControllerBase
    {
        private readonly IOutsourceApproveService service;

        public OutsourceController(
            IOutsourceApproveService service)
        {
            this.service = service;
        }

        [HttpGet("Approve/{documentBaseId:long}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetIsApprovedAsync(
            [FromRoute] long documentBaseId,
            [FromQuery] DateTime initialDate)
        {
            var result = await service.GetIsApprovedAsync(documentBaseId, initialDate);

            return new ApiDataResult(result);
        }

        [HttpPost("Approve/GetByIds")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetIsApprovedAsync(
            OutsourceBatchApproveRequestDto requestDto)
        {
            var result = await service.GetIsApprovedAsync(requestDto.DocumentBaseIds, requestDto.InitialDate);
            var response = OutsourceMapper.Map(result);

            return new ApiDataResult(response);
        }

        [HttpPost("Approve/GetIsAllOperationsApproved")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetIsAllOperationsApprovedAsync(
            [FromBody] OutsourceAllOperationsApprovedRequestDto dto, CancellationToken ct)
        {
            var request = OutsourceMapper.Map(dto);
            var result = await service.GetIsAllOperationsApprovedAsync(request, ct);

            return new ApiDataResult(result);
        }

        [HttpPut("Approve/{documentBaseId:long}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetIsApprovedAsync(
            [FromRoute] long documentBaseId,
            [FromBody] OutsourceApproveRequestDto request)
        {
            await service.SetIsApprovedAsync(documentBaseId, request.IsApproved, request.InitialDate);

            return Ok();
        }

        [HttpPost("Approve")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SetIsApprovedAsync(
            [FromBody] OutsourceBatchApproveRequestDto requestDto)
        {
            await service.SetIsApprovedAsync(requestDto.DocumentBaseIds);

            return Ok();
        }
    }
}