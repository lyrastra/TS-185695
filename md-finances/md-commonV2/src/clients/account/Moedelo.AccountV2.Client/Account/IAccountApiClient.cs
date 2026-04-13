using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.Account;

namespace Moedelo.AccountV2.Client.Account
{
    public interface IAccountApiClient
    {
        /// <summary> 
        /// Возвращает аккаунт пользователя
        /// </summary>
        Task<AccountDto> GetAccountByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        
        /// <summary> 
        /// Возвращает аккаунты пользователей
        /// </summary>
        Task<Dictionary<int, AccountDto>> GetAccountsByUserIdsAsync(IReadOnlyCollection<int> userIds, CancellationToken cancellationToken = default);
        
        /// <summary> 
        /// Возвращает список аккаунтов по списку индентификаторов
        /// </summary>
        Task<List<AccountDto>> GetByIdsAsync(IReadOnlyCollection<int> accountIds);

        /// <summary>
        /// Запрос на объединение аккаунтов
        /// </summary>
        Task<int> SaveUnionRequestAsync(UnionRequestDto unionRequest);

        /// <summary>
        /// Удаление акаунта
        /// </summary>
        Task DeleteAccountAsync(int accountId);

        /// <summary>
        /// Возможно ли объединение аккаунтов
        /// </summary>
        Task<bool> CanSendUnionRequestAsync(int firmId, int userId);

        /// <summary>
        /// Сохраняет аккаунт пользователя
        /// </summary>
        /// <returns>возвращает id сохранённого аккаунта</returns>
        Task<int> SaveAccountAsync(AccountDto accountDto);

        /// <summary>
        /// Создаёт аккаунт от главной фирмы к подчинённой
        /// </summary>
        Task<int> CreateAccountAsync(int firmId, int userId, AccountDto accountDto);

        /// <summary>
        /// ОБъединение аккантов
        /// </summary>
        Task<MergeAccountsResultDto> MergeAccountsAsync(MergeAccountsRequestDto requestDto);
    }
}