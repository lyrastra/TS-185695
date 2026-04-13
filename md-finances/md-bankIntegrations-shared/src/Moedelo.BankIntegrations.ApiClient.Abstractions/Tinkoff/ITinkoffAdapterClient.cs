using Moedelo.BankIntegrations.ApiClient.Dto.Tinkoff;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.BankIntegrations.ApiClient.Dto.Accounts;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Tinkoff
{
    public interface ITinkoffAdapterClient
    {
        Task<TinkoffClientInfoDto> GetClientInfoAsync(string code, string redirectUri);
        Task<GetAccountsResponseDto> GetAccounts(int firmId);
        Task<int> SaveIntegrationDataAsync(IntegrationDataDto integrationDataDto);
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
        Task<RequestMovementResponseDto> MonitoringRequestMovementAsync(RequestMovementRequestDto request);
    }
}
