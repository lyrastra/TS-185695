using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(ITransferToAccountApiClient))]
    public class TransferToAccountApiClient : BaseApiClient, ITransferToAccountApiClient
    {
        public TransferToAccountApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<TransferToAccountApiClient> logger)
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

        public async Task<PaymentOrderSaveResponseDto> CreateAsync(TransferToAccountSaveDto createRequest)
        {
            var url = "/api/v1/PaymentOrders/Outgoing/TransferToAccount";
            var result = await PostAsync<TransferToAccountSaveDto, ApiDataDto<PaymentOrderSaveResponseDto>>(url, createRequest);
            return result.data;
        }
    }
}
