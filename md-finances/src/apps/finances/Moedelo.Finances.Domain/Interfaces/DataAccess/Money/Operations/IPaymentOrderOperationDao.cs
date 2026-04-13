using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations
{
    public interface IPaymentOrderOperationDao : IDI
    {
        Task<List<long>> GetBaseIdsForUncategorizedAsync(int firmId, long? sourceId);

        Task<List<PaymentOrderOperation>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);

        Task<List<long>> GetDuplicateBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);

        Task<List<PaymentOrderOperation>> GetForReconciliationAsync(int firmId, int settlementAccountId, DateTime startDate, DateTime endDate);

        Task<List<PaymentOrderOperation>> GetBudgetaryPaymentsAsync(int firmId, BudgetaryPaymentOrderOperationQueryParams queryParams);

        Task<List<PaymentOrderOperation>> GetFor1cConfirmationAsync(DateTime startDate, DateTime endDate);

        Task<DateTime?> GetLastDateUntilAsync(int firmId, DateTime date);

        Task<List<PaymentOrderStatus>> GetStatusesByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);

        Task<PaymentOrderOperation> GetIncomingBalanceOperationAsync(int firmId, int settlementAccountId);

        Task<bool> HasOperationsBySettlementAccountAsync(int firmId, int settlementAccountId);

        Task<List<PaymentOrderOperation>> GetBudgetaryPaymentsWithUnifiedTaxPaymentsAsync(int firmId, BudgetaryPaymentOrderOperationQueryParams queryParams);
    }
}