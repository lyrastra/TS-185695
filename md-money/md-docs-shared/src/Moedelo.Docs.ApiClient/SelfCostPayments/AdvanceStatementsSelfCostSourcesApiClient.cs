using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostPayments.AdvanceStatements.Models;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostPayments.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.AdvanceStatements;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SelfCostSources.AdvanceStatements
{
    [InjectAsSingleton(typeof(IAdvanceStatementSelfCostPaymentsApiClient))]
    public class AdvanceStatementsSelfCostPaymentsApiClient : BaseApiClient, IAdvanceStatementSelfCostPaymentsApiClient
    {
        public AdvanceStatementsSelfCostPaymentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AdvanceStatementsSelfCostPaymentsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AdvanceStatementsApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyCollection<AdvanceStatemenSelfCostPaymentDto>> GetOnDateAsync(SelfCostPaymentRequestDto request)
        {
            var url = $"/private/api/v1/SelfCostPayments/GetOnDate";
            var result = await PostAsync<SelfCostPaymentRequestDto, DataResponse<IReadOnlyCollection<AdvanceStatemenSelfCostPaymentDto>>>(url, request);
            return result.Data;
        }
    }
}