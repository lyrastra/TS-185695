using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation
{
    public interface IReconciliationService : IDI
    {
        Task<ReconciliationResult> GetLastInfoAsync(int firmId, int settlementAccountId);
        Task<ReconciliationResult[]> GetLastWithDiffInfoAsync(int firmId, ReconciliationStatus reconciliationStatus, IReadOnlyCollection<int> settlementAccountIds);
        Task<ReconciliationBusinessModel> GetLastAsync(IUserContext userContext, int? settlementAccountId = null);
        Task<ReconciliationBusinessModel> GetBySessionIdAsync(IUserContext userContext, Guid sessionId);
        Task CompleteAsync(IUserContext userContext, Guid sessionId, IReadOnlyCollection<long> excessOperations, IReadOnlyCollection<long> missingOperations);
        Task CancelAsync(int firmId, Guid sessionId);
        Task<bool> IsAnyInProgressAsync(int firmId, int settlementAccountId);
        Task<Guid?> GetLastSessionInProcessAsync(int firmId, int settlementAccountId);
        Task<BalanceReconcilation[]> GetByDateAsync(int firmId, IReadOnlyCollection<int> settlementAccountIds, DateTime date);
    }
}
