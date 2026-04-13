using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Finances.ApiClient.Abstractions.Legacy;
using Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Finances.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IIntegrationStatementsApiClient))]
    public class IntegrationStatementsApiClient : BaseLegacyApiClient, IIntegrationStatementsApiClient
    {
        public IntegrationStatementsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IntegrationStatementsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FinancesPrivateApiEndpoint"),
                logger)
        {
        }

        public Task<ResultOfStatementRequestDto[]> RequestAsync(FirmId firmId, UserId userId, StatementRequestDto request)
        {
            return PostAsync<StatementRequestDto, ResultOfStatementRequestDto[]>(
                $"/Integrations/Statements/Request?firmId={firmId}&userId={userId}", 
                request);
        }
    }
}