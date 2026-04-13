using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/fa8e6b18b72caf1d76f51ca55517c21481208644/src/clients/requisites/Moedelo.RequisitesV2.Client/SettlementAccounts/ISettlementAccountClient.cs#L8
    /// </summary>
    public interface ISettlementAccountApiClient
    {
        /// <summary>
        /// Возвращает список расчетных счетов (за исключением удаленных)
        /// </summary>
        Task<SettlementAccountDto[]> GetAsync(FirmId firmId, UserId userId);
        
        Task<SettlementAccountDto> GetByIdAsync(FirmId firmId, UserId userId, int settlementAccountId);

        Task<SettlementAccountDto> GetPrimaryAsync(FirmId firmId, UserId userId);

        Task<SettlementAccountDto[]> GetByNumbersAsync(FirmId firmId, UserId userId, IReadOnlyCollection<string> numbers);

        Task<SettlementAccountDto[]> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> settlementAccountIds);

        Task<IReadOnlyDictionary<int, SettlementAccountDto[]>> GetByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken = default);

        Task<SettlementAccountDto[]> GetWithDeletedAsync(FirmId firmId, UserId userId);

        Task<SavedSettlementAccountDto> SaveAsync(FirmId firmId, UserId userId, SettlementAccountDto settlementAccount);

        Task ArchiveAsync(FirmId firmId, UserId userId, int settlementAccountId);
        
        Task DearchiveAsync(FirmId firmId, UserId userId, int settlementAccountId);
    }
}