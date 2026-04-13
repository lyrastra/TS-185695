using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SuiteCrm.Dto.Bpm;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class CrmBpmApiClient : BaseApiClient, ICrmBpmApiClient
    {
        private readonly HttpQuerySetting setting = new HttpQuerySetting(new TimeSpan(0, 0, 10, 0));
        private readonly SettingValue apiEndPoint;

        public CrmBpmApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SuiteCrmApiUrl");
        }

        public Task<List<BpmNotifyLeadInfo>> GetInfoNoStatementNoZeroNoIntegrationsAsync(IEnumerable<int> ids)
        {
            var sids = string.Join(",", ids.Select(i => i.ToString()));
            return GetAsync<List<BpmNotifyLeadInfo>>("/BpmNotification/GetLeadsNoIntegrationsNoActivity",
                new {ids = sids});
        }

        public Task<List<BpmNotifyLeadInfo>> GetInfoNoStatementAsync(IEnumerable<int> ids)
        {
            var sids = string.Join(",", ids.Select(i => i.ToString()));
            return GetAsync<List<BpmNotifyLeadInfo>>("/BpmNotification/GetLeads", new {ids = sids});
        }

        public Task ResetStatementsStatusesAsync()
        {
            return PostAsync("/BpmNotification/ResetStatementsStatuses");
        }

        public Task<List<AccountantInfoDto>> GetAccountantInfoAsync(IEnumerable<int> firmIds)
        {
            return PostAsync<IEnumerable<int>, List<AccountantInfoDto>>("/Bpm/Account/AccountantInfo", firmIds);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}