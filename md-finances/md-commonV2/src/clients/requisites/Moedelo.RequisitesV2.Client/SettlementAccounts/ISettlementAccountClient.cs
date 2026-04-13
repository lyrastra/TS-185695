using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;

namespace Moedelo.RequisitesV2.Client.SettlementAccounts
{
    public interface ISettlementAccountClient : IDI
    {
        Task<List<SettlementAccountDto>> GetAsync(int firmId, int userId);
        
        Task<List<SettlementAccountDto>> GetAsync(int firmId, int userId, CancellationToken cancellationToken);

        Task<List<SettlementAccountDto>> GetWithDeletedAsync(int firmId, int userId);
        /// <summary> возвращает список всех счетов как есть без подмены на рублевый при отсутствии разрешения на валютный
        Task<List<SettlementAccountDto>> GetSettlementAccountListAsync(int firmId, int userId);

        Task<SettlementAccountDto> GetPrimaryAsync(int firmId, int userId);

        Task<SettlementAccountDto> GetByIdAsync(int firmId, int userId, int id);

        Task<bool> ExistsForeignCurrencyAccountsAsync(int firmId, int userId);

        Task<List<SettlementAccountDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids);

        Task<List<SettlementAccountDto>> GetByNumbersAsync(int firmId, int userId, IReadOnlyCollection<string> numbers);

        Task<SavedSettlementAccountDto> SaveAsync(int firmId, int userId, SettlementAccountDto settlementAccount);
        /// <returns>Код ошибки. 0-все в порядке</returns>
        Task<int> ValidateSettlementAccountAsync(int firmId, int userId, SettlementAccountDto settlementAccount);

        Task DearchiveAsync(int firmId, int userId, int id);

        Task<List<SettlementAccountDto>> GetBySubcontoIdsAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds);

        Task<List<SettlementAccountDto>> GetAutocompleteAsync(
            int firmId,
            int userId,
            string query,
            int count,
            IReadOnlyCollection<int> exceptIds = null,
            SettlementAccountType settlementAccountType = SettlementAccountType.Default);

        Task<bool> ExistsAnyAsync(int firmId, int userId);

        Task ArchiveAsync(int firmId, int userId, int id);
    }
}