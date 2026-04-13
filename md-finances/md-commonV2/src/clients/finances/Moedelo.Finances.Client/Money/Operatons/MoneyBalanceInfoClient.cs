using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Finances.Client.Money
{
    [InjectAsSingleton]
    public class MoneyBalanceInfoClient : BaseApiClient, IMoneyBalanceInfoClient
    {
        private readonly SettingValue apiEndpoint;

        public MoneyBalanceInfoClient(
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

        public Task<Dictionary<int, bool>> GetIsBalanceSettedForFirmsAsync(List<int> firmIds)
        {
            return PostAsync<List<int>, Dictionary<int, bool>>($"/Money/Operations/Incoming/BalanceOperations/GetIsBalanceSettedForFirms", firmIds);
        }
    }
}
