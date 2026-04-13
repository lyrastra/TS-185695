using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.legacy;

namespace Moedelo.PaymentOrderImport.ApiClient.legacy
{

    [InjectAsSingleton(typeof(IPaymentImportQueueClient))]
    class PaymentImportQueueClient : BaseApiClient, IPaymentImportQueueClient
    {
        private const string prefix = "/private/api/v1";
        
        public PaymentImportQueueClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentImportQueueClient> logger)
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

        public Task AppendBPMEventAsync(int firmId, string fileId)
        {
            return PostAsync($"{prefix}/PaymentImportQueue/Append/bpm", new { firmId, fileId });
        }
        
        
    }
}