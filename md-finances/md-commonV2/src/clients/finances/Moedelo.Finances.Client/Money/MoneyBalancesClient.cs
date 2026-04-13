using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Client.Money.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Finances.Client.Money
{
    [InjectAsSingleton]
    public class MoneyBalancesClient : BaseApiClient, IMoneyBalancesClient
    {
        private readonly SettingValue apiEndpoint;

        public MoneyBalancesClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FinancesPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<MoneySourceBalanceDto>> GetAsync(int firmId, int userId, BalanceRequestDto request)
        {
            return PostAsync<BalanceRequestDto, List<MoneySourceBalanceDto>>($"/Money/Balances?firmId={firmId}&userId={userId}", request);
        }

        public Task ReconcileWithServiceAsync(int firmId, int userId, ReconcileRequestDto request)
        {
            return PostAsync($"/Money/Balances/ReconcileWithService?firmId={firmId}&userId={userId}", request);
        }
    }
}
