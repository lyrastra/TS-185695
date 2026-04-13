using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationError;
using Moedelo.BankIntegrations.ApiClient.BankIntegrationsNetCore;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.IntegrationError
{
    [InjectAsSingleton(typeof(IIntegrationErrorApiClient))]
    public class IntegrationErrorApiClient : BaseApiClient, IIntegrationErrorApiClient
    {
        private const string ControllerName = "/private/api/v1/IntegrationError";

        public IntegrationErrorApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BankOperationClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApiNetCore"),
                logger)
        {
        }

        public async Task ReadUnreadByPartnerAsync(ReadUnreadIntegrationErrorRequestDto dto)
        {
            await PutAsync($"{ControllerName}/readUnreadByPartner", dto);
        }

        public async Task SaveAsync(IntegrationErrorSaveRequestDto dto, CancellationToken cancellationToken = default)
        {
            await PostAsync($"{ControllerName}/Save", dto, cancellationToken: cancellationToken);
        }
    }
}
