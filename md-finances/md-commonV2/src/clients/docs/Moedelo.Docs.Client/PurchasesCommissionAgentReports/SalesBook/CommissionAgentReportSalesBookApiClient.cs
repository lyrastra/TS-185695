using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Common;
using Moedelo.Docs.Dto.PurchasesCommissionAgentReports.SalesBook;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.PurchasesCommissionAgentReports.SalesBook
{
    [InjectAsSingleton(typeof(ICommissionAgentReportSalesBookApiClient))]
    public class CommissionAgentReportSalesBookApiClient : BaseCoreApiClient, ICommissionAgentReportSalesBookApiClient
    {
        private readonly SettingValue apiEndpoint;

        public CommissionAgentReportSalesBookApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("CommissionAgentReportsApiEndpoint");
        }

        public async Task<IReadOnlyCollection<SalesBookSummaryResponseDto>> GetSummaryAsync(
            int firmId,
            int userId,
            SalesBookSummaryRequestDto requestDto,
            CancellationToken ct)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, ct).ConfigureAwait(false);

            var result = await PostAsync<SalesBookSummaryRequestDto, ApiDataResult<SalesBookSummaryResponseDto[]>>(
                "/private/api/v1/SalesBook/GetSummary",
                requestDto,
                queryHeaders: tokenHeaders,
                cancellationToken: ct);

            return result.data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
