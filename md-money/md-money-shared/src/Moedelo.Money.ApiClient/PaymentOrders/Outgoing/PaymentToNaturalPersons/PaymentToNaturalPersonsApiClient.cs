using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsApiClient))]
    public class PaymentToNaturalPersonsApiClient : BaseApiClient, IPaymentToNaturalPersonsApiClient
    {
        public PaymentToNaturalPersonsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentToNaturalPersonsApiClient> logger)
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

        public async Task<PaymentToNaturalPersonsResponseDto[]> GetByBaseIdsIdAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var url = $"/private/api/v1/PaymentOrders/Outgoing/PaymentToNaturalPersons/GetByBaseIds";
            var result = await PostAsync<IReadOnlyCollection<long>, ApiDataDto<PaymentToNaturalPersonsResponseDto[]>>(
                url, documentBaseIds);

            return result.data;
        }
    }
}