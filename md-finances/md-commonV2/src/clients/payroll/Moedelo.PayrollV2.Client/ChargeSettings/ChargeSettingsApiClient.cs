using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.ChargeSettings;

namespace Moedelo.PayrollV2.Client.ChargeSettings
{
    [InjectAsSingleton]
    public class ChargeSettingsApiClient: BaseApiClient, IChargeSettingsApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public ChargeSettingsApiClient(
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
            return apiEndPoint.Value + "/ChargeSettings";
        }

        public Task<List<SalaryTemplateDto>> GetSalaryTemplatesAsync(int firmId, int userId,
            SalaryTemplatesRequestDto request)
        {
            return PostAsync<SalaryTemplatesRequestDto, List<SalaryTemplateDto>>(
                $"/GetSalaryTemplates?firmId={firmId}&userId={userId}", request);
        }
    }
}