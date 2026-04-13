using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.PayrollV2.Dto.RosstatReport;

namespace Moedelo.PayrollV2.Client.Reports.Rosstat
{
    public interface IRosstatReportApiClient : IDI
    {
        Task<WorkersCountForRosstatDto> GetWorkersCountAsync(int firmId, int userId, int year);

        Task<WorkersSalaryForRosstatDto> GetWorkersSalaryAsync(int firmId, int userId, int year);
    }
}
