using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.InitIntegration;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrations.Models.Movement;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IIntegratedFileClient))]
    public class IntegratedFileClient : BaseApiClient, IIntegratedFileClient
    {
        private const string ControllerName = "IntegratedFiles";

        public IntegratedFileClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IntegratedFileClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApi"),
                logger)
        {
        }

        public async Task<bool> TransferFilesToMdAsync(MDMovementList movementList, int firmId, IntegrationPartners integrationPartner, string requestId)
        {
            var response = await PostAsync<MDMovementList, DataResponseWrapper<bool>>($"/{ControllerName}/TransferFilesToMd?firmId={firmId}&integrationPartner={integrationPartner}&requestId={requestId}", movementList);
            return response.Data;
        }
    }
}
