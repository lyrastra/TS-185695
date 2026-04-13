using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.Rsv;
using Moedelo.PayrollV2.Dto.Szv;

namespace Moedelo.PayrollV2.Client.Reports.SzvExperience
{
    public interface ISzvExperienceReportApiClient : IDI
    {
        Task<List<SzvWorkerModelDto>> GetWorkersAsync(int firmId, int userId, int year);
        Task<List<SzvValidationResultDto>> GetPeriodErrorsListAsync(int firmId, int userId, int year);
        Task<List<WorkerInfoCheckDto>> GetWorkersForCheckAsync(int firmId, int userId, int year);
    }
}