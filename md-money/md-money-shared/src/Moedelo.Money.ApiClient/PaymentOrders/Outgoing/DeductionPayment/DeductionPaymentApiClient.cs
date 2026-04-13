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
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.DeductionPayment;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.DeductionPayment.Models;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.DeductionPayment
{
    [InjectAsSingleton(typeof(IDeductionPaymentApiClient))]
    public class DeductionPaymentApiClient : BaseApiClient, IDeductionPaymentApiClient
    {
        public DeductionPaymentApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<DeductionPaymentApiClient> logger)
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

        public async Task<PaymentOrderSaveResponseDto> CreateAsync(DeductionPaymentSaveDto dto)
        {
            const string url = "/api/v1/PaymentOrders/Outgoing/Deduction";
            var result = await PostAsync<DeductionPaymentSaveDto, ApiDataDto<PaymentOrderSaveResponseDto>>(url, dto);
            return result.data;
        }

        public Task DeleteByDocumentBaseIdAsync(long documentBaseId)
        {
            const string url = "/api/v1/PaymentOrders/Outgoing/Deduction";
            return DeleteAsync(url, new { documentBaseId });
        }
    }
}