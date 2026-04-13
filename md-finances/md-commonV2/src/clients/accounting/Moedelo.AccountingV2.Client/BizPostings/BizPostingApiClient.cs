using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.BizPostings
{
    [InjectAsSingleton]
    public class BizPostingApiClient : BaseApiClient, IBizPostingApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public BizPostingApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingsRepository) : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingsRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<BizPostingDto>> GetBizPostingsForReconciliationStatementAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            var settings = new HttpQuerySetting(new TimeSpan(0, 10, 0));
            var result = await GetAsync<GetBizPostingsForReconciliationStatementResponse>("/BizPostingApi/GetBizPostingsForReconciliationStatement", new { firmId, userId, startDate, endDate }, null, settings).ConfigureAwait(false);
            return result.Items;
        }

        public async Task<List<BizPostingReportDto>> GetBizPostingReportAsync(int firmId, int userId, DateTime startDate, DateTime endDate, bool isReconciliationStatement)
        {
            var result = await GetAsync<GetBizPostingReportResponse>("/BizPostingApi/GetBizPostingReport", new { firmId, userId, startDate, endDate, isReconciliationStatement }).ConfigureAwait(false);
            return result.Items;
        }
    }
}
