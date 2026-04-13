using Moedelo.Finances.Client.Money.Dtos;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;
using Moedelo.Finances.Dto.Money;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.Finances.Client.Money
{
    [InjectAsSingleton]
    public class MoneyReconciliationClient : BaseApiClient, IMoneyReconciliationClient
    {
        private const string ControllerName = "/Money/Reconciliation";
        private readonly SettingValue apiEndpoint;

        public MoneyReconciliationClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FinancesPrivateApiEndpoint");
        }

        public Task ReconcileForBackofficeAsync(int firmId, int userId, ReconciliationForBackofficeRequestDto request)
        {
            return PostAsync($"/ForBackoffice?firmId={firmId}&userId={userId}", request);
        }

        public Task ReconcileForUserAsync(int firmId, int userId, ReconciliationForUserRequestDto request)
        {
            return PostAsync($"/ForUser?firmId={firmId}&userId={userId}", request);
        }

        public Task<ReconciliationResponseDto> GetLastAsync(int firmId, int userId, int settlementAccountId)
        {
            return GetAsync<ReconciliationResponseDto>($"/Last", new {firmId, userId, settlementAccountId});
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + ControllerName;
        }
    }
}
