using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using System.Threading.Tasks;

namespace Moedelo.LinkedDocuments.ApiClient.BaseDocuments
{
    [InjectAsSingleton(typeof(IPaymentReserveBaseDocumentsClient))]
    internal sealed class PaymentReserveBaseDocumentsClient : BaseApiClient, IPaymentReserveBaseDocumentsClient
    {
        public PaymentReserveBaseDocumentsClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentReserveBaseDocumentsClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("LinkedDocumentsApiEndpoint"),
                logger)
        {
        }

        public async Task<BaseDocumentDto> GetOrCreateAsync()
        {
            var uri = "/api/v1/BaseDocuments/PaymentReserve";
            var response = await PostAsync<DataResponse<BaseDocumentDto>>(uri).ConfigureAwait(false);
            return response.Data;
        }
    }
}