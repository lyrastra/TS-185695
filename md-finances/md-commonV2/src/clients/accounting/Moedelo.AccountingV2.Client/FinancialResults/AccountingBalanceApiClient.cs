using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.FinancialResults;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.FinancialResults
{
    [InjectAsSingleton]
    public class AccountingBalanceApiClient : BaseApiClient, IAccountingBalanceApiClient
    {
        private readonly SettingValue apiEndPoint;

        public AccountingBalanceApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi") ;
        }

        public async Task<FinancialResultsDto> GetFinancialResultsAsync(int firmId, int userId, int wizardStateId)
        {
            return (await GetAsync<DataResponseWrapper<FinancialResultsDto>>("/GetFinancialResults", new { firmId, userId, wizardStateId }).ConfigureAwait(false)).Data;
        }

        public async Task<decimal> GetAdvanceStatementDebtsByWorkerAsync(int firmId, int userId, int workerId)
        {
            return (await GetAsync<DataResponseWrapper<decimal>>("/GetAdvanceStatementDebtsByWorker", new { firmId, userId, workerId }).ConfigureAwait(false)).Data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/AccountingBalance";
        }
    }
}
