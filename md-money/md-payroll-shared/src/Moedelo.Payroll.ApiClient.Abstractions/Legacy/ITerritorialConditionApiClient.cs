using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.TerritorialCondition;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy;

public interface ITerritorialConditionApiClient
{
    Task<IReadOnlyCollection<WorkerTerritorialConditionOnDateResponseDto>> GetByWorkersOnDateAsync(
        int firmId, int userId, IReadOnlyCollection<TerritorialConditionOnDateRequestDto> dtos);
}