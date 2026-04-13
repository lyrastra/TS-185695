using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequestsBan;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationRequestsBan;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.ApiClient.Framework.IntegrationRequestsBan
{
    [InjectAsSingleton(typeof(IIntegrationRequestsBanApiClient))]
    public class IntegrationRequestsBanApiClient : BaseCoreApiClient, IIntegrationRequestsBanApiClient
    {
        private readonly SettingValue endpoint;
        private const string ControllerName = "/private/api/v1/IntegrationsRequestBan";

        public IntegrationRequestsBanApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.endpoint = settingRepository.Get("IntegrationApiNetCore");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public async Task SetIsAllowedAsync(SetIsAllowedIntegrationRequestsBanRequestDto dto)
        {
            await PutAsync(
                    $"{ControllerName}/SetIsAllowed",
                    dto,
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}
