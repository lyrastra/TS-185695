using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Kontragents;
using Moedelo.Finances.Domain.Models.Money.Operations.MoneyTransfers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations
{
    public interface IMoneyTransferOperationDao : IDI
    {
        Task<List<MoneyTransferOperation>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);

        Task<List<KontragentTurnover>> TopByOperationsWithKontragentsAsync(int firmId, int count, DateTime startDate, DateTime endDate);
        Task<List<MoneyTransferOperation>> GetForReconciliationAsync(int firmId, int settlementAccountId, DateTime startDate, DateTime endDate);

        Task<MoneyTransferOperation> GetIncomingBalanceOperationAsync(int firmId, int settlementAccountId);

        Task<bool> HasOperationsBySettlementAccountAsync(int firmId, int settlementAccountId);

        Task<int> InsertAsync(int firmId, MoneyTransferOperation operation);
        Task UpdateAsync(int firmId, MoneyTransferOperation operation);

        Task DeleteByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);

        Task<Dictionary<int, bool>> GetIsBalanceSettedForFirmsAsync(IReadOnlyCollection<int> firmIds);
    }
}