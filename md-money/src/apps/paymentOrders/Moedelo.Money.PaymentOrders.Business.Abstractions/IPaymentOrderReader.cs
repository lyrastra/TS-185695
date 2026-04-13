using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions
{
    public interface IPaymentOrderReader
    {
        Task<PaymentOrderResponse> GetByBaseIdAsync(long documentBaseId, OperationType operationType);
        Task<IReadOnlyCollection<PaymentOrderResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds, OperationType operationType);
        Task<IReadOnlyList<long>> GetBaseIdsByOperationTypeAsync(OperationType operationType, int? year);
        Task<long> GetOperationIdAsync(long documentBaseId);
        Task<bool> GetIsFromImportAsync(long documentBaseId);
        Task<PaymentOrderDuplicateDataResponse> GetDuplicateDataAsync(long documentBaseId);
        Task<OperationType> GetOperationTypeAsync(long documentBaseId);
        Task<long> GetBaseIdByIdAsync(long id);
        Task<IReadOnlyList<OperationTypeResponse>> GetOperationTypeByIdsAsync(IReadOnlyCollection<long> documentBaseIds);
        Task<IReadOnlyList<int>> GetOutgoingNumbersAsync(int settlementAccountId, int? year, int? cut);
        Task<bool> GetIsPaidAsync(long documentBaseId);
        Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusAsync(DocumentsStatusRequest request);
        Task<PaymentOrderSnapshot> GetPaymentSnapshotAsync(long documentBaseId);
    }
}