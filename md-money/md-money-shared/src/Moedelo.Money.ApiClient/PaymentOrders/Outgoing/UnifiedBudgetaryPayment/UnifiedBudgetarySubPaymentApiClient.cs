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
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Dto;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetarySubPaymentApiClient))]
    public class UnifiedBudgetarySubPaymentApiClient: BaseApiClient, IUnifiedBudgetarySubPaymentApiClient
    {
        public UnifiedBudgetarySubPaymentApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UnifiedBudgetarySubPaymentApiClient> logger)
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
        
        public async Task<long> GetParentIdAsync(long documentBaseId)
        {
            var url = $"/private/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/SubPayments/{documentBaseId}/ParentId";
            var result = await GetAsync<ApiDataDto<long>>(url);
            return result.data;
        }

        public async Task<IReadOnlyCollection<UnifiedBudgetarySubPaymentResponsePrivateDto>> GetByParentIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var url = $"/private/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/SubPayments/GetByParentIds";
            var result = await PostAsync<IReadOnlyCollection<long>, ApiDataDto<IReadOnlyCollection<UnifiedBudgetarySubPaymentResponsePrivateDto>>>(url, documentBaseIds);
            return result.data;
        }
    }
}