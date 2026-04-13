using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Finances.ApiClient.Abstractions.Legacy.Integration;
using Moedelo.Finances.ApiClient.Abstractions.Legacy.Integration.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Finances.ApiClient.Legacy.Integration
{
    [InjectAsSingleton(typeof(IIntegrationPaymentOrderApiClient))]
    internal class IntegrationPaymentOrderApiClient : BaseLegacyApiClient, IIntegrationPaymentOrderApiClient
    {
        public IntegrationPaymentOrderApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IntegrationPaymentOrderApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FinancesPrivateApiEndpoint"),
                logger)
        {
        }

        public Task<SendPaymentOrdersResponseDto> SendAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds)
        {
            return PostAsync<IReadOnlyCollection<long>, SendPaymentOrdersResponseDto>(
                $"/Integrations/PaymentOrder/Send?firmId={firmId}&userId={userId}",
                documentBaseIds);
        }
    }
}
