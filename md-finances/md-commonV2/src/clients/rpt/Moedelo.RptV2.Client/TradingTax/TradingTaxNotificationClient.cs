using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto;

namespace Moedelo.RptV2.Client.TradingTax
{
    [InjectAsSingleton]
    public class TradingTaxNotificationClient : BaseApiClient, ITradingTaxNotificationClient
    {
        private readonly SettingValue apiEndpoint;

        public TradingTaxNotificationClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }

        public async Task<int[]> GetIncompleteWizardTradingObjectsIdsAsync(int firmId, int userId, DateTime beforeDate)
        {
            return (await GetAsync<DataWrapper<int[]>>($"/TradingTaxNotification/GetIncompleteWizardTradingObjectsIdsBeforeDate", new { firmId, userId, beforeDate }).ConfigureAwait(false)).Data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
