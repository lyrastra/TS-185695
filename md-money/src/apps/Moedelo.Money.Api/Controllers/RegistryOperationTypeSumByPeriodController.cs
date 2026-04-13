using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.Registry.OperationTypeSumByPeriod;
using Moedelo.Money.Business.Abstractions.RegistryOperationTypeSumByPeriod;

namespace Moedelo.Money.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("private/api/v{version:apiVersion}/[controller]")]
[HasAllAccessRules(AccessRule.UsnAccountantTariff)]
public class RegistryOperationTypeSumByPeriodController(
    IRegistryOperationTypeSumByPeriodReader reader) : ControllerBase
{
    /// <summary>
    /// Получение сумм по операциям за период сгруппированных по типу операции
    /// </summary>
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<OperationTypeSumByPeriodResponseDto[]>))]
    [SwaggerOperation(Tags = new[] { "Деньги - Реестр операций" })]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> PostAsync(OperationTypeSumByPeriodRequestDto dto, CancellationToken ct)
    {
        var request = RegistryOperationTypeSumByPeriodMapper.MapToDomain(dto);
        var result = await reader.GetAsync(request, ct);
        var response = RegistryOperationTypeSumByPeriodMapper.MapToDto(result);
        return new ApiDataResult(response);
    }
}
