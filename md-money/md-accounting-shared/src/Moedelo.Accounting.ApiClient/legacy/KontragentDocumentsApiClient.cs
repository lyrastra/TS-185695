using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.KontragentDocuments.ReconciliationStatements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IKontragentDocumentsApiClient))]
    public class KontragentDocumentsApiClient : BaseLegacyApiClient, IKontragentDocumentsApiClient
    {
        public KontragentDocumentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<KontragentDocumentsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }
        
        public Task<IReadOnlyList<ReportTableCalculationDataDto>> GetReconciliationStatementDataForEdmAsync(int firmId, int userId, ReconciliationStatementRequestDto dto) =>
            PostAsync<ReconciliationStatementRequestDto, IReadOnlyList<ReportTableCalculationDataDto>>(
            $"/KontragentDocuments/GetReconciliationStatementDataForEdm?firmId={firmId}&userId={userId}", dto);
    }
}