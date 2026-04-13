using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IEmployeesApiClient
    {
        Task<WorkerDto> GetByIdAsync(FirmId firmId, UserId userId, int workerId);

        Task<List<WorkerDto>> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds);

        Task<WorkerDto> GetDirectorAsync(FirmId firmId, UserId userId);

        Task<WorkerCardAccountDto> GetWorkerCardAccountAsync(FirmId firmId, UserId userId, int workerId);

        Task<List<WorkerCardAccountDto>> GetWorkersCardAccountAsync(FirmId firmId, UserId userId, IEnumerable<int> workerIds);
    }
}
