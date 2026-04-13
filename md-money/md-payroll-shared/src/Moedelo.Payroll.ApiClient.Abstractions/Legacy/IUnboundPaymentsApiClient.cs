using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Audit;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IUnboundPaymentsApiClient
    {
        Task<List<AuditUnboundPaymentsWorkerDto>> GetUnboundPaymentsWorkersAsync(int firmId, int userId,
            DateTime startDate, DateTime endDate, IReadOnlyCollection<int> workerIds);

        Task<List<AuditUnboundPaymentsWorkerDto>> GetUnboundPaymentsWorkersAsync(AuditUnboundPaymentsRequest request);
    }
}