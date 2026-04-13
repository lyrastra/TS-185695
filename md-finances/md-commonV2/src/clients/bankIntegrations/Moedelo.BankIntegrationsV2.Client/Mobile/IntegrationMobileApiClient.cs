using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.Mobile;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.Mobile
{
    [InjectAsSingleton]
    public class IntegrationMobileApiClient : BaseApiClient, IIntegrationForMobileApiClient
    {
        private readonly SettingValue apiEndPoint;

        public IntegrationMobileApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        public async Task<SettlementAccountBalancesResponseDto> GetSettlementAccountsBalancesAsync(int firmId, SettlementAccountBalanceRequestDto settlementAccounts)
        {
            return (await PostAsync<SettlementAccountBalanceRequestDto, SettlementAccountBalancesResponseDto>(
                "/Mobile/GetSettlementAccountsBalances", settlementAccounts).ConfigureAwait(false));
        }

        public async Task<IntegrationMobileResponseDto<CachePaymentListDto>> GetSettlementAccountOperationsAsync(int firmId, string settlmentAccountNumber)
        {
            return (await GetAsync<IntegrationMobileResponseDto<CachePaymentListDto>>("/Mobile/GetCachePayments",
                new {firmId, settlmentAccountNumber}).ConfigureAwait(false));
        }

        public async Task<bool> DeleteFirmBalanceAsync(int firmId, int settlementAccountId)
        {
            return (await PostAsync<DataResponseWrapper<bool>>($"/Mobile/DeleteFirmBalance?firmId={firmId}&settlementAccountId={settlementAccountId}").ConfigureAwait(false)).Data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}