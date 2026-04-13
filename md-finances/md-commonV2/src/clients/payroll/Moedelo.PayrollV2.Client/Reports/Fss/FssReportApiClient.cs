using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.Fss;

namespace Moedelo.PayrollV2.Client.Reports.Fss
{
    [InjectAsSingleton]
    public class FssReportApiClient : BaseApiClient, IFssReportApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public FssReportApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }
        
        protected override string GetApiEndpoint()
        {
            return $"{apiEndPoint.Value}/FssReport";
        }

        public Task<ChargedAndPaidFeesStepDto> GetChargedAndPaidFeesStepAsync(int firmId, int userId, int period, 
            int year, ChargedAndPaidFeesStepDto prevData = null)
        {
            return PostAsync<ChargedAndPaidFeesStepRequestDto, ChargedAndPaidFeesStepDto>(
                $"/GetChargedAndPaidFeesStep?firmId={firmId}&userId={userId}",
                new ChargedAndPaidFeesStepRequestDto {Period = period, Year = year, PrevData = prevData});
        }

        public Task<InsuranceCasesStepDto> GetInsuranceCasesStepAsync(int firmId, int userId, int period, int year, 
            InsuranceCasesStepDto prevData = null)
        {
            return PostAsync<InsuranceCasesStepRequestDto, InsuranceCasesStepDto>(
                $"/GetInsuranceCasesStep?firmId={firmId}&userId={userId}",
                new InsuranceCasesStepRequestDto {Period = period, Year = year, PrevData = prevData});
        }

        public Task<MedicalCheckStepDto> GetMedicalCheckStepAsync(int firmId, int userId, int period, int year, 
            MedicalCheckStepDto prevData = null)
        {
            return PostAsync<MedicalCheckStepRequestDto, MedicalCheckStepDto>(
                $"/GetMedicalCheckStep?firmId={firmId}&userId={userId}",
                new MedicalCheckStepRequestDto {Period = period, Year = year, PrevData = prevData});
        }

        public Task<bool> HasPreviousYearOldReportAsync(int firmId, int userId, int year)
        {
            return GetAsync<bool>("/HasPreviousYearOldReport", new { firmId, userId, year });
        }

        public Task<int> GetPaidWorkerCountAsync(int firmId, int userId, int period, int year)
        {
            return GetAsync<int>("/GetPaidWorkerCount", new { firmId, userId, period, year });
        }
    }
}