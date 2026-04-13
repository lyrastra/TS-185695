using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.Fss;

namespace Moedelo.PayrollV2.Client.Reports.Fss
{
    public interface IFssReportApiClient : IDI
    {
        Task<ChargedAndPaidFeesStepDto> GetChargedAndPaidFeesStepAsync(int firmId, int userId, int period, int year,
            ChargedAndPaidFeesStepDto prevData = null);

        Task<InsuranceCasesStepDto> GetInsuranceCasesStepAsync(int firmId, int userId, int period, int year,
            InsuranceCasesStepDto prevData = null);

        Task<MedicalCheckStepDto> GetMedicalCheckStepAsync(int firmId, int userId, int period, int year,
            MedicalCheckStepDto prevData = null);
        
        Task<bool> HasPreviousYearOldReportAsync(int firmId, int userId, int year);
        
        Task<int> GetPaidWorkerCountAsync(int firmId, int userId, int period, int year);
    }
}
