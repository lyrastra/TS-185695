using Moedelo.BankIntegrations.ApiClient.Dto.Blancbank;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountDto = Moedelo.BankIntegrations.ApiClient.Dto.Blancbank.AccountDto;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Blancbank
{
    public interface IBlancbankAdapterClient
    {
        /// <summary>Получение токена по коду и сохранение в базу</summary>
        /// <param name="redirectUri">Бланку зачем-то нужен в POST'е redirectUri</param>
        Task<bool> WriteTokenByCodeAsync(int firmId, string code, string codeVerifier, string redirectUri);
        
        /// <summary>Создание согласия на получение счетов</summary>
        /// <returns>consentId идентификатор согласия</returns>
        Task<string> CreateAccountConsentsAsync(int firmId);

        /// <summary>Список счетов в согласии</summary>
        Task<List<AccountDto>> GetAccountsInConsentAsync(int firmId, string consentId);

        /// <summary>Получение выписок</summary>
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);

        Task<BaseResponseDto> SaveIntegrationDataAsync(IntegrationDataDto integrationDataDto);

        Task<CompanyDetailsDto> GetCompanyDetailsAsync(int firmId, string accountId);

    }
}
