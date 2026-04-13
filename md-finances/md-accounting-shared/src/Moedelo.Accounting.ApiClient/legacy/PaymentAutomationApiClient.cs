using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IPaymentAutomationApiClient))]
    internal sealed class PaymentAutomationApiClient : BaseLegacyApiClient, IPaymentAutomationApiClient
    {
        public PaymentAutomationApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentAutomationApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<ReasonDocumentDto[]> GetReasonDocumentsAsync(FirmId firmId, UserId userId,
            FindReasonDocumentsAutomationDto dto)
        {
            return PostAsync<FindReasonDocumentsAutomationDto, ReasonDocumentDto[]>(
                $"/PaymentAutomation/GetReasonDocuments?firmId={firmId}&userId={userId}", dto);
        }
    }
}