using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Dto;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderNumerationApiClient))]
    public class PaymentOrderNumerationApiClient : BaseApiClient, IPaymentOrderNumerationApiClient
    {
        public PaymentOrderNumerationApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentOrderNumerationApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyNumerationApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyList<int>> GetNextNumbersAsync(int settlementAccountId, int year, int count)
        {
            if (settlementAccountId == 0 || year == 0 || count == 0)
            {
                return null;
            }
            var response = await GetAsync<ApiDataDto<IReadOnlyList<int>>>("/api/v1/PaymentOrderNumeration/NextNumbers", new
            {
                settlementAccountId,
                year,
                count
            });

            return response.data;
        }
    }
}
