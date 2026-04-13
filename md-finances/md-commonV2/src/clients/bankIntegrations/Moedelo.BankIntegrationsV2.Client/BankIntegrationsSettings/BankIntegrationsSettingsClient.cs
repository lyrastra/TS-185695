using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.BankIntegrationsSettings
{
    [InjectAsSingleton]
    public class BankIntegrationsSettingsClient : BaseApiClient, IBankIntegrationsSettingsClient
    {
        private const string ControllerName = "/Settings/";
        private readonly SettingValue apiEndPoint;

        public BankIntegrationsSettingsClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public async Task<bool> IsMockAllRequestErrorAsync()
        {
            try
            {
                return await GetAsync<bool>("IsMockAllRequestError").ConfigureAwait(false);
            }
            catch
            {
                return true;
            }
        }

        public Task SetMockAllRequestErrorAsync(bool value)
        {
            var param = $"?value={value}";
            return PostAsync($"SetMockAllRequestError{param}");
        }
    }
}