using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Client.SalarySettings.DTO;

namespace Moedelo.PayrollV2.Client.SalarySettings
{
    [InjectAsSingleton]
    public class SalarySettingsApiClient : BaseApiClient, ISalarySettingsApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public SalarySettingsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                 )
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/SalarySettings";
        }

        public Task<SalarySettingPrivateClientModel> GetSalarySettingData(int firmId, int userId)
        {
            return GetAsync<SalarySettingPrivateClientModel>("/GetSalarySettingData", new { firmId, userId });
        }

        public Task SetPilotProjectStartDate(int firmId, int userId)
        {
            return PostAsync($"/SetPilotProjectStartDate?firmId={firmId}&userId={userId}");
        }

        public Task SaveSalarySettingData(int firmId, int userId, SavingSalarySettings settingData)
        {
            return PostAsync($"/SaveSalarySettingData?firmId={firmId}&userId={userId}", settingData);
        }

        public Task<SalarySettingDto> GetSalarySetting(int firmId, int userId)
        {
            return GetAsync<SalarySettingDto>("/GetSalarySetting", new { firmId, userId });
        }
    }
}