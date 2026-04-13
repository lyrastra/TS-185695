using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentApiClient))]
    public class BudgetaryPaymentApiClient : BaseApiClient, IBudgetaryPaymentApiClient
    {
        public BudgetaryPaymentApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BudgetaryPaymentApiClient> logger)
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

        public async Task<BudgetaryPaymentDto> GetByIdAsync(long documentBaseId)
        {
            var url = $"/api/v1//PaymentOrders/Outgoing/BudgetaryPayment/{documentBaseId}";
            var result = await GetAsync<ApiDataDto<BudgetaryPaymentDto>>(url);
            return result.data;
        }

        public async Task<PaymentOrderSaveResponseDto> CreateAsync(BudgetaryPaymentSaveDto dto)
        {
            const string url = "/api/v1//PaymentOrders/Outgoing/BudgetaryPayment";
            var result = await PostAsync<BudgetaryPaymentSaveDto, ApiDataDto<PaymentOrderSaveResponseDto>>(url, dto);
            return result.data;
        }
    }
}