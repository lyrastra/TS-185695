using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.PayrollV2.Dto.SzvmReport;

namespace Moedelo.PayrollV2.Client.Reports.Szvm
{
    public interface ISzvmReportApiClient : IDI
    {
        Task<List<WorkerForSzvmDto>> GetWorkersAsync(int firmId, int userId, 
            DateTime startDate, DateTime endDate, IReadOnlyCollection<int> ids);
    }
}
