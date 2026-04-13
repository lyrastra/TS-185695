using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Registry.Api.Mappers;
using Moedelo.Money.Registry.Business.Abstractions;
using Moedelo.Money.Registry.Dto.OperationTypeSumByPeriod;

namespace Moedelo.Money.Registry.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("private/api/v{version:apiVersion}/[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class OperationTypeSumByPeriodController(
    IOperationTypeSumByPeriodReader reader) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(200)]
    public async Task<IActionResult> PostAsync(OperationTypeSumByPeriodRequestDto dto, CancellationToken ct)
    {
        var request = OperationTypeSumByPeriodMapper.MapToDomain(dto);
        var result = await reader.GetAsync(request, ct);
        var response = OperationTypeSumByPeriodMapper.MapToResponse(result);
        return new ApiDataResult(response);
    }
}
