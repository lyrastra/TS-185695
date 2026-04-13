using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions
{
    public interface IPaymentOrderDao
    {
        Task<PaymentOrder> GetAsync(int firmId, long documentBaseId);

        Task<IReadOnlyList<PaymentOrder>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentBaseIds, OperationType operationType);

        Task<IReadOnlyList<long>> GetBaseIdsByOperationTypeAsync(int firmId, OperationType operationType, int? year);

        Task<long?> GetBaseIdByIdAsync(int firmId, long id);

        Task<bool> GetIsFromImportAsync(int firmId, long documentBaseId);

        Task<long> InsertAsync(PaymentOrder operation);

        Task UpdateAsync(PaymentOrder operation);

        Task DeleteAsync(int firmId, long documentBaseId);

        Task<HashSet<long>> DeleteInvalidAsync(int firmId, IReadOnlyCollection<long> documentBaseIds);

        Task<IReadOnlyList<OperationTypeResponse>> GetOperationTypeByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentBaseIds);

        Task ApplyIgnoreNumberAsync(int firmId, IReadOnlyCollection<long> documentBaseIds);

        Task<IReadOnlyList<PaymentOrder>> GetByOperationStateAsync(int firmId, OperationState operationState);

        Task<HashSet<long>> ApproveImportedAsync(int firmId, int? settlementAccountId, DateTime? skipline);
    }
}
