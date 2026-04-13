using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.Other;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.Other.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.PaymentOrders.Incoming.OtherIncoming
{
    [InjectAsSingleton(typeof(IOtherIncomingApiClient))]
    public class OtherIncomingApiClientApiClient : BaseApiClient, IOtherIncomingApiClient
    {
        public OtherIncomingApiClientApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<OtherIncomingApiClientApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyApiEndpoint"),
                logger)
        {
        }

        public async Task<OtherIncomingDto> GetByBaseIdAsync(long documentBaseId)
        {
            var url = $"/api/v1/PaymentOrders/Incoming/Other/{documentBaseId}";
            var result = await GetAsync<ApiDataDto<OtherIncomingDto>>(url);
            return result.data;
        }

        public async Task<OtherIncomingDto[]> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var url = $"/private/api/v1/PaymentOrders/Incoming/Other/GetByBaseIds";
            var result = await PostAsync<IReadOnlyCollection<long>, ApiDataDto<OtherIncomingDto[]>>(url, documentBaseIds);
            return result.data;
        }
    }
}