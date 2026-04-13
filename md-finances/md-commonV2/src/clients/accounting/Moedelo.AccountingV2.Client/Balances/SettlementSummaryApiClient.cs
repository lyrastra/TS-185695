using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Balances;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Balances
{
    [InjectAsSingleton]
    public class SettlementSummaryApiClient : BaseApiClient, ISettlementSummaryApiClient
    {
        private readonly SettingValue apiEndPoint;

        public SettlementSummaryApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
        
        public Task<List<SettlementBalanceDto>> GetAsync(int firmId, int userId, DateTime? onDate)
        {
            return GetAsync<List<SettlementBalanceDto>>("/SettlementSummary/Get", new { firmId, userId, onDate });
        }
    }
}