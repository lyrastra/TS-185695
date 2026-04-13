using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.Mobile;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BankIntegrationsV2.Client.Mobile
{
    public interface IIntegrationForMobileApiClient : IDI
    {
        Task<SettlementAccountBalancesResponseDto> GetSettlementAccountsBalancesAsync(int firmId, SettlementAccountBalanceRequestDto settlementAccounts);
        Task<IntegrationMobileResponseDto<CachePaymentListDto>> GetSettlementAccountOperationsAsync(int firmId, string settlmentAccountNumber);
        Task<bool> DeleteFirmBalanceAsync(int firmId, int settlementAccountId);
    }
}