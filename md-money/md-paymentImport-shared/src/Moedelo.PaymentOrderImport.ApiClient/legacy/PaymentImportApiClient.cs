using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.legacy;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

namespace Moedelo.PaymentOrderImport.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IPaymentImportApiClient))]
    class PaymentImportApiClient : BaseApiClient, IPaymentImportApiClient
    {
        public PaymentImportApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentImportApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("PaymentImportApiEndpoint"),
                logger)
        {
        }

        public Task ImportFromIntegrationAsync(FirmId firmId, UserId userId, string fileId, bool isManual = false, HttpQuerySetting setting = null)
        {
            return PostAsync($"/ImportFromIntegration?firmId={firmId}&userId={userId}&fileId={fileId}&isManual={isManual}", setting: setting);
        }

        public Task ImportFromUserAsync(FirmId firmId, UserId userId, ImportFromUserDto dto, HttpQuerySetting setting = null)
        {
            return PostAsync($"/ImportFromUser?firmId={firmId}&userId={userId}", dto, setting: setting);
        }

        public Task AddImportMessageAsync(FirmId firmId, string message, HttpQuerySetting setting = null)
        {
            return PostAsync<object>($"/AddImportMessage?firmId={firmId}", new { Message = message }, setting: setting);
        }
    }
}
