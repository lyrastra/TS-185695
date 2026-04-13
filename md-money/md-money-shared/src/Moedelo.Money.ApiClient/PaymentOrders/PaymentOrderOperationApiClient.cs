using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderOperationApiClient))]
    internal sealed class PaymentOrderOperationApiClient : BaseApiClient, IPaymentOrderOperationApiClient
    {
        public PaymentOrderOperationApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentOrderApiClient> logger)
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
        
        public Task ApproveImportedAsync(ApproveImportedOperationsRequestDto dto)
        {
            if (dto.SourceType != null &&
                dto.SourceType != MoneySourceType.SettlementAccount)
            {
                return Task.CompletedTask;
            }

            var url = "/api/v1/Operations/Imported/Approve";
            return PostAsync(url, dto);
        }
    }
}