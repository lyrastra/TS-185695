using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.PaymentOrder;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.ApiClient.MovementHash;
using Moedelo.BankIntegrations.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.PaymentOrder;

[InjectAsSingleton(typeof(IPaymentOrderClient))]
public class PaymentOrderClient : BaseApiClient, IPaymentOrderClient
{
    public PaymentOrderClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<PaymentOrderClient> logger) : base(
        httpRequestExecuter,
        uriCreator,
        auditTracer,
        authHeadersGetter,
        auditHeadersGetter,
        settingRepository.Get("PaymentOrderApiEndpoint"),
        logger)
    {
    }
    
    public async Task<SendPaymentOrderResponseDto> SendPaymentOrder(SendPaymentOrderRequestDto data)
    {
        var response = await PostAsync<SendPaymentOrderRequestDto, ApiDataResult<SendPaymentOrderResponseDto>>(
            uri: "/private/api/v1/PaymentOrder/SendPaymentOrder",
            data: data);

        return response.data;
    }
}