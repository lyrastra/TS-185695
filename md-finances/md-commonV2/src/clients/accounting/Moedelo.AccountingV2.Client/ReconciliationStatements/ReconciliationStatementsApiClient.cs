using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.ReconciliationStatements;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.ReconciliationStatements
{
    [InjectAsSingleton]
    public class ReconciliationStatementsApiClient : BaseApiClient, IReconciliationStatementsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public ReconciliationStatementsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository repository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = repository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<ReconciliationStatementReportDto>> CreateAsync(int firmId, int userId, ReconciliationStatementQueryDto queryDto)
        {
            var result = await PostAsync<ReconciliationStatementQueryDto, ReconciliationStatementResponseDto>(
                $"/ReconciliationStatement/Create?firmId={firmId}&userId={userId}",
                queryDto).ConfigureAwait(false);
            return result.Items;
        }

        public async Task<decimal> GetKontragentDebtAsync(int firmId, int userId, int kontragentId, HttpQuerySetting setting = null)
        {
            var result = await GetAsync<DataResponseWrapper<decimal>>(
               $"/ReconciliationStatement/GetAccountingKontragentDebt?firmId={firmId}&userId={userId}&kontragentId={kontragentId}", 
               setting: setting)
               .ConfigureAwait(false);
            return result.Data;
        }
    }
}
