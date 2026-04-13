using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource;

namespace Moedelo.Money.Api.Controllers.PaymentOrders.Outsource;

[ApiController]
[ApiVersion("1")]
[Route("private/api/v{version:apiVersion}/PaymentOrders/Outsource")]
[ApiExplorerSettings(IgnoreApi = true)]
public class OutsourcePrivateController(
    IOutsourceApproveService approveService,
    IOutsourceDeleteService deleteService) : ControllerBase
{
    [HttpPost("Approve/GetIsAllOperationsApproved")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetIsAllOperationsApproved(
        OutsourceAllOperationsApprovedRequestDto dto, CancellationToken ct)
    {
        var request = OutsourceMapper.Map(dto);
        var result = await approveService.GetIsAllOperationsApprovedAsync(request, ct);

        return new ApiDataResult(result);
    }

    [HttpPost("Delete/{documentBaseId:long}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteAsync([FromRoute]long documentBaseId)
    {
        var result = await deleteService.DeleteAsync(documentBaseId);
        return new ApiDataResult(result.ToDto());
    }
}