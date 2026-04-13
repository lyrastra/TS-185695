using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Reconcilation
{
    public interface IBalanceReconcilationDao : IDI
    {
        Task<ReconciliationResult> GetLastAsync(int firmId, int? settlementAccountId = null);
        Task<ReconciliationResult[]> GetLastBySettlementAccountIdsAsync(int firmId, ReconciliationStatus reconciliationStatus, IReadOnlyCollection<int> settlementAccountIds);
        Task<ReconciliationResult> GetBySessionIdAsync(int firmId, Guid sessionId);
        Task InsertAsync(int firmId, BalanceReconcilation reconcilation);
        Task SetStatusAsync(int firmId, Guid sessionId, ReconciliationStatus status);
        Task UpdateAsync(int firmId, ReconciliationResult reconciliationResult);
        Task<DateTime?> GetMaxCreateDateAsync(int firmId, int settlementAccountId);
        Task SetReadyToCompleteAsync(int firmId, int settlementAccountId);
        Task<bool> IsAnyInProgressAsync(int firmId, int settlementAccountId);
        Task<ISet<int>> GetSettlementAccountsInProgressAsync(int firmId, IReadOnlyCollection<int> settlementAccountIds);
        Task<Guid?> GetLastSessionInProcessAsync(int firmId, int settlementAccountId);
        Task<List<BalanceReconcilation>> GetByDateAsync(int firmId, IReadOnlyCollection<int> settlementAccountIds, DateTime date);
    }
}
