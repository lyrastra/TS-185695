using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.IntegrationError;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.IntegrationError
{
    [InjectAsSingleton]
    public class IntegrationErrorClient : BaseApiClient, IIntegrationErrorClient
    {
        private const string ControllerName = "/IntegrationError/";
        private readonly SettingValue apiEndPoint;

        public IntegrationErrorClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task<IntegrationErrorListDto> GetListAsync(IntegrationErrorRequestDto dto)
        {
            return GetAsync<IntegrationErrorListDto>("GetList", dto);
        }

        public Task SetReadStateAsync(IntegrationErrorSetReadDto dto)
        {
            return PostAsync("SetReadState", dto);
        }
    }
}