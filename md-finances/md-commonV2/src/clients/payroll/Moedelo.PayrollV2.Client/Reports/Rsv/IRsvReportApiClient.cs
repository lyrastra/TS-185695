using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.Rsv;

namespace Moedelo.PayrollV2.Client.Reports.Rsv
{
    public interface IRsvReportApiClient : IDI
    {
        Task<List<TariffDto>> GetPfrAndFfomsStepAsync(int firmId, int userId, int period, int year,
            IReadOnlyCollection<TariffDto> prevData = null);

        Task<FssDto> GetFssStepAsync(int firmId, int userId, int period, int year, FssDto prevData = null);

        Task<LowTariffDto> GetLowTariffStepAsync(int firmId, int userId, int period, int year,
            LowTariffDto prevData = null);
        
        Task<List<WorkerInfoCheckDto>> GetWorkersForCheckAsync(int firmId, int userId, int period, int year);

        Task<List<WorkerInfoDto>> GetWorkersStepAsync(int firmId, int userId, int period, int year, 
            IReadOnlyCollection<WorkerInfoDto> prevData = null);
        
        Task<PatentLowTariffDto> UsePatentLowTariffAsync(int firmId, int userId, int period, int year);
    }
}
