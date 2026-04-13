using System.Threading.Tasks;
using Moedelo.Finances.Dto.Money.Operations;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Finances.Client.Money
{
    [InjectAsSingleton]
    public class MoneyIncomingBalanceOperationClient : BaseApiClient, IMoneyIncomingBalanceOperationClient
    {
        private readonly SettingValue apiEndpoint;

        public MoneyIncomingBalanceOperationClient(
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

        public Task<MoneyIncomingBalanceOperationDto> GetAsync(int firmId, int userId, int settlementAccountId)
        {
            return GetAsync<MoneyIncomingBalanceOperationDto>($"/Money/Operations/Incoming/BalanceOperations", new { firmId, userId, settlementAccountId });
        }

        public Task SaveAsync(int firmId, int userId, MoneyIncomingBalanceOperationDto operation)
        {
            return PostAsync($"/Money/Operations/Incoming/BalanceOperations?firmId={firmId}&userId={userId}", operation);
        }

        public Task DeleteAsync(int firmId, int userId, int settlementAccountId)
        {
            return DeleteAsync($"/Money/Operations/Incoming/BalanceOperations?firmId={firmId}&userId={userId}&settlementAccountId={settlementAccountId}");
        }
    }
}
