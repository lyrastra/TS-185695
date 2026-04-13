using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Balances;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Balances
{
    public interface IBalancesApiClient : IDI
    {
        Task<DateTime?> GetDateAsync(int firmId, int userId, bool useReadOnlyDb = false, CancellationToken cancellationToken = default);
        Task<List<BalanceDto>> GetByAccountCodesAsync(int firmId, int userId, IReadOnlyCollection<SyntheticAccountCode> codes);
        Task<IReadOnlyDictionary<int, AccountBalanceDto[]>> GetByFirmIdsAndAccountCodesAsync(GetBalanceByFirmIdsRequestDto request);
        Task<List<BalanceDto>> GetBySubcontoIdsAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds);
        Task ReplaceKontragentsInResultsAsync(int firmId, int userId, KontragentsReplaceInResultsDto request);
        Task<string> GetPeriodNameAsync(int firmId, int userId, long id);
    }
}