using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Calendar;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto.WizardEngine;

namespace Moedelo.RptV2.Client.WizardEngine
{
    [InjectAsSingleton]
    public class WizardEngineClient : BaseApiClient, IWizardEngineClient
    {
        private readonly SettingValue apiEndpoint;

        public WizardEngineClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }

        public Task<ChangeStatusResultDto> CompleteWizardAsync(int firmId, int userId, int eventId, CalendarEventType eventType)
        {
            return GetAsync<ChangeStatusResultDto>("/WizardEngineApi/CompleteWizard", new { firmId, userId, eventId, eventType });
        }

        public Task<ChangeStatusResultDto> ReopenWizardAsync(int firmId, int userId, int eventId, CalendarEventType eventType, bool confirmed)
        {
            return GetAsync<ChangeStatusResultDto>("/WizardEngineApi/ReopenWizard", new { firmId, userId, eventId, eventType, confirmed });
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
