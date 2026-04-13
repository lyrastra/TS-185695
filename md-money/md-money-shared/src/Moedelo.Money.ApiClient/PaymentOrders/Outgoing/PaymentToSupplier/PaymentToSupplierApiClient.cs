using System.Threading;
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
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Dto;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierApiClient))]
    internal class PaymentToSupplierApiClient : BaseApiClient, IPaymentToSupplierApiClient
    {
        private const string Url = "/api/v1//PaymentOrders/Outgoing/PaymentToSupplier";

        public PaymentToSupplierApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentToSupplierApiClient> logger)
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

        public async Task<PaymentOrderSaveResponseDto> CreateAsync(PaymentToSupplierSaveDto dto)
        {
            var result = await PostAsync<PaymentToSupplierSaveDto, ApiDataDto<PaymentOrderSaveResponseDto>>(Url, dto);
            return result.data;
        }

        public Task DeleteAsync(int companyId, long paymentBaseId, CancellationToken ct)
        {
            return DeleteAsync($"{Url}/{paymentBaseId}?_companyId={companyId}", cancellationToken: ct);
        }
    }
}