using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SuiteCrm.Dto.Synchronization;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class CrmSynchronizationApiClient : BaseApiClient, ICrmSynchronizationApiClient
    {
        private readonly TimeSpan maxTimeout = new TimeSpan(0, 10, 0);
        private readonly SettingValue apiEndPoint;
        
        public CrmSynchronizationApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, 
            IResponseParser responseParser, 
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SuiteCrmApiUrl");
        }

        public Task<SyncResultDto> SyncAccountAsync(int firmId)
        {
            return PostAsync<SyncResultDto>(
                $"/Sync/SyncAccount?firmId={firmId}",
                setting: new HttpQuerySetting
                {
                    Timeout = maxTimeout
                });
        }

        public Task SyncAccontWithPayAsync()
        {
            return PostAsync("/Sync/SyncAccontWithPay");
        }

        public Task SyncAccountsChangesAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync(
                "/Sync/SyncAccountsChanges",
                firmIds,
                setting: new HttpQuerySetting
                {
                    Timeout = maxTimeout
                });
        }

        public Task<List<int>> GetAccountsFirmIdsAsync()
        {
            return GetAsync<List<int>>(
                "/Sync/GetAccountsFirmIds",
                setting: new HttpQuerySetting
                {
                    Timeout = maxTimeout
                });
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}
