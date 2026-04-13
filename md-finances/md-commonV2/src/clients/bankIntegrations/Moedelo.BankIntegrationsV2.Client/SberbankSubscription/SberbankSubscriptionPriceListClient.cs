using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.SberbankSubscription
{
    [InjectAsSingleton]
    class SberbankSubscriptionPriceListClient : BaseApiClient, ISberbankSubscriptionPriceListClient
    {
        private const string ControllerName = "/SberbankSubscriptionPriceList/";
        private readonly SettingValue apiEndPoint;

        public SberbankSubscriptionPriceListClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task<int> GetPriceListIdByTariffAndMonthCountAsync(int tariffId, int monthCount)
        {
            return GetAsync<int>("GetPriceListIdByTariffAndMonthCount", new { tariffId, monthCount });
        }
    }
}
