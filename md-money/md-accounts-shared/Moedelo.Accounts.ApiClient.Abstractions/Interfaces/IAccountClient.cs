using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Dto;

namespace Moedelo.Accounts.Abstractions.Interfaces
{
    public interface IAccountClient
    {
        /// <summary> 
        /// Возвращает аккаунт пользователя
        /// </summary>
        Task<AccountDto> GetAccountByUserIdAsync(int userId);

        Task<Dictionary<int, AccountDto>> GetByUserIdsAsync(IReadOnlyCollection<int> userIds);
        
        /// <summary> 
        /// Возвращает список аккаунтов по списку индентификаторов
        /// </summary>
        Task<List<AccountDto>> GetByIdsAsync(IReadOnlyCollection<int> accountIds);

        /// <summary>
        /// Возвращает идентификаторы фирм и id админа аккаунта по accountId
        /// </summary>
        Task<AccountFirmsDto> GetUserFirmsInfoByAccountId(int accountId);
    }
}