using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Statements;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Statements.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.RelinkSources.Statements
{
    [InjectAsSingleton(typeof(IStatementRelinkSourceApiClient))]
    public class StatementRelinkSourceApiClient : BaseApiClient, IStatementRelinkSourceApiClient
    {
        public StatementRelinkSourceApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<StatementRelinkSourceApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("StatementsApiEndpoint"),
                logger)
        {
        }
        
        public async Task<IReadOnlyCollection<PurchaseStatementRelinkDto>> GetPurchasesAsync(RelinkSourceRequestDto request)
        {
            var url = $"/private/api/v1/RelinkSources/Purchases";
            var result = await PostAsync<RelinkSourceRequestDto, DataResponse<IReadOnlyCollection<PurchaseStatementRelinkDto>>> (url, request);
            return result.Data;
        }

        public async Task<IReadOnlyCollection<SaleStatementRelinkDto>> GetSalesAsync(RelinkSourceRequestDto request)
        {
            var url = $"/private/api/v1/RelinkSources/Sales";
            var result = await PostAsync<RelinkSourceRequestDto, DataResponse<IReadOnlyCollection<SaleStatementRelinkDto>>> (url, request);
            return result.Data;
        }
    }
}