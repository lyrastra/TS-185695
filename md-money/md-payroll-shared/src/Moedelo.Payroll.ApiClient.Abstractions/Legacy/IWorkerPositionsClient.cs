using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerPositions;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IWorkerPositionsClient
    {
        Task UpdateOrdersAsync(int firmId, int userId, List<PositionHistoryOrderUpdatingDto> dto);

        Task<WorkerPositionOnFireDateDto> GetWorkerPositionOnFireDateAsync(int firmId, int userId, int workerId);

        Task AssignDivisionAsync(FirmId firmId, UserId userId, AssignDivisionDto assignDivision,
            CancellationToken token = default);

        Task<IReadOnlyCollection<WorkerPositionDto>> GetByCriteriaAsync(FirmId firmId, UserId userId,
            ByCriteriaRequestDto request, CancellationToken token);

        Task<IReadOnlyCollection<DepartmentPositionResponseDto>> GetDepartmentsWithPositionsAsync(FirmId firmId,
            UserId userId);
    }
}