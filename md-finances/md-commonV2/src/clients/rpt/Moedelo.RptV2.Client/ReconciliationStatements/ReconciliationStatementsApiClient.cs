using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto;
using Moedelo.RptV2.Dto.ReconciliationStatements;

namespace Moedelo.RptV2.Client.ReconciliationStatements
{
    [InjectAsSingleton]
    public class ReconciliationStatementsApiClient : BaseApiClient, IReconciliationStatementsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public ReconciliationStatementsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository repository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = repository.Get("RptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<List<ReconciliationStatementReportDto>> CreateAsync(int firmId, int userId, ReconciliationStatementQueryDto queryDto)
        {
            var result = await PostAsync<ReconciliationStatementQueryDto, ReconciliationStatementResponseDto>(
                $"/ReconciliationStatement/Create?firmId={firmId}&userId={userId}",
                queryDto).ConfigureAwait(false);
            return result.Items;
        }

        public async Task<List<ReconcilationStatementFileDto>> GetReportFilesByKontragentIdsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            var results =  await PostAsync<IReadOnlyCollection<int>, ListDto<ReconcilationStatementFileDto>>(
                $"/ReconciliationStatement/GetReportFileByKontragentIds?firmId={firmId}&userId={userId}", kontragentIds)
                .ConfigureAwait(false);
            return results.Items;
        }

        public async Task<decimal> GetKontragentDebtAsync(int firmId, int userId, int kontragentId, HttpQuerySetting setting = null)
        {
            var results = await GetAsync<DataWrapper<decimal>>(
                $"/ReconciliationStatement/GetBizKontragentDebt?firmId={firmId}&userId={userId}&kontragentId={kontragentId}", setting: setting)
                .ConfigureAwait(false);
            return results.Data;
        }
    }
}
