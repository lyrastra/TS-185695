using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.Accounts;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Mtsbank
{
    public interface IMobileTelesystemsBankAdapterClient
    {
        /// <summary>
        /// Создает заявку на подключение
        /// </summary>
        /// <returns>ApplicationId</returns>
        public Task<string> CreateRequestOnIntegrationAsync(string inn, string login);
        public Task<GetAccountsResponseDto> GetAccountsAsync(string customerId);
        public Task<Dto.MobileTelesystemsBank.AccountDto> GetAccountAsync(string customerId, string account);
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
        Task<RequestMovementResponseDto> MonitoringRequestMovementListAsync(RequestMovementRequestDto requestDto);
    }
}
