using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IWorkerEmploymentChangesClient
    {
        Task<List<WorkerEmploymentChangeDto>> GetRecruitmentInfosAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate);

        Task<List<WorkerEmploymentChangeDto>> GetPositionChangesAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate);

        Task<List<WorkerEmploymentChangeDto>> GetDismissalsAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate);

        Task<List<FullWorkerEmploymentChangesDto>> GetFullDataAsync(int firmId, int userId, List<int> workerIds, DateTime beforeDate);

        Task<List<FullWorkerEmploymentChangesDto>> GetLastChangesOnDateAsync(int firmId, int userId,
            List<int> workerIds, DateTime beforeDate);

        Task<List<int>> CheckWorkerChangesInPeriodAsync(int firmId, int userId, IReadOnlyCollection<int> workerIds,
            DateTime startDate, DateTime endDate);

        Task<List<int>> GetWorkerIdsWithoutChangesInPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
    }
}