using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Dto;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.PaymentOrderMlOperationType;
using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.PaymentOrderMlOperationType
{
    [InjectAsSingleton(typeof(IPaymentOrderMlOperationTypeApiClient))]
    public class PaymentOrderMlOperationTypeApiClient : BaseApiClient, IPaymentOrderMlOperationTypeApiClient
    {
        public PaymentOrderMlOperationTypeApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentOrderMlOperationTypeApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("PaymentImportHandlerApiEndpoint"),
                logger)
        {
        }

        public async Task<PaymentImportOperationType?> VerifyAsync(
            VerifyOperationTypeDto request,
            CancellationToken ct)
        {

            var result = await PostAsync<VerifyOperationTypeDto, ApiDataDto<PaymentImportOperationType?>>(
                "/private/api/v1/PaymentOrderMlOperationType/Verify", request, cancellationToken: ct);

            return result?.data;
        }

        public Task SetSkipStatus(
            SetMlSkipStatusDto request, CancellationToken ct)
        {
            return Task.CompletedTask;
        }
    }
}
