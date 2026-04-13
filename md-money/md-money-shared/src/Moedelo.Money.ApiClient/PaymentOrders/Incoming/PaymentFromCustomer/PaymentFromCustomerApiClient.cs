using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;

namespace Moedelo.Money.ApiClient.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerApiClient))]
    public class PaymentFromCustomerApiClient : BaseApiClient, IPaymentFromCustomerApiClient
    {
        public PaymentFromCustomerApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentFromCustomerApiClient> logger)
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

        public async Task<PaymentFromCustomerDto> GetByIdAsync(long documentBaseId)
        {
            var url = $"/api/v1/PaymentOrders/Incoming/PaymentFromCustomer/{documentBaseId}";
            var result = await GetAsync<ApiDataDto<PaymentFromCustomerDto>>(url);
            return result.data;
        }

        public async Task<IReadOnlyCollection<PaymentFromCustomerDto>> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var url = $"/private/api/v1/PaymentOrders/Incoming/PaymentFromCustomer/GetByBaseIds";
            var result = await PostAsync<IReadOnlyCollection<long>, ApiDataDto<IReadOnlyCollection<PaymentFromCustomerDto>>>(url, documentBaseIds);
            return result.data;
        }
    }
}