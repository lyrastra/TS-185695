using Microsoft.Extensions.Logging;
using Moedelo.AccountingStatements.ApiClient.Abstractions;
using Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.PaymentForDocuments;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.AccountingStatements.ApiClient
{
    [InjectAsSingleton(typeof(IPaymentForDocumentClient))]
    public class PaymentForDocumentClient : BaseApiClient, IPaymentForDocumentClient
    {
        public PaymentForDocumentClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentForDocumentClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AccountingStatementsApiEndpoint"),
                logger)
        {
        }

        public async Task<PaymentForDocumentCreateResponseDto> CreateAsync(PaymentForDocumentCreateRequestDto request)
        {
            var response = await PostAsync<IReadOnlyCollection<PaymentForDocumentCreateRequestDto>, DataResponseWrapper<PaymentForDocumentCreateResponseDto[]>>(
                "/private/api/v1/PaymentForDocument", new[] { request });
            return response.Data.Single();
        }

        public async Task<PaymentForDocumentCreateResponseDto[]> CreateAsync(IReadOnlyCollection<PaymentForDocumentCreateRequestDto> requests)
        {
            HttpQuerySetting setting = new HttpQuerySetting(TimeSpan.FromSeconds(100));
            var response = await PostAsync<IReadOnlyCollection<PaymentForDocumentCreateRequestDto>, DataResponseWrapper<PaymentForDocumentCreateResponseDto[]>>(
                "/private/api/v1/PaymentForDocument", requests, null, setting).ConfigureAwait(false);
            return response.Data;
        }

        public Task DeleteAsync(IReadOnlyCollection<long> documentBaseIds, bool deleteLinksImmediately = false, HttpQuerySetting setting = null)
        {
            return DeleteByRequestAsync(
                $"/private/api/v1/PaymentForDocument?deleteLinksImmediately={deleteLinksImmediately}",
                documentBaseIds,
                setting: setting
            );
        }
    }
}
