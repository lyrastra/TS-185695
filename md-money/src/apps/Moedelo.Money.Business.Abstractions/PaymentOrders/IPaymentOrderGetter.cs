using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Snapshot;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface IPaymentOrderGetter
    {
        Task<OperationType> GetOperationTypeAsync(long documentBaseId);

        Task<long> GetOperationIdAsync(long documentBaseId);

        Task<long> GetOperationBaseIdAsync(long id);

        Task<OperationIsFromImportResponse> GetIsFromImportAsync(long documentBaseId);

        Task<DuplicateDataResponse> GetDuplicateDataAsync(long documentBaseId);

        Task<OperationTypeResponse[]> GetOperationTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);

        Task<ReportFile> GetReportAsync(long documentBaseId, ReportFormat format);

        Task<IReadOnlyList<long>> GetBaseIdsByOperationTypeAsync(OperationType operationType, int? year);

        Task<IReadOnlyList<int>> GetOutgoingNumbersAsync(int settlementAccountId, int? year, int? cut);

        Task<bool> GetIsPaidAsync(long documentBaseId);

        Task<PaymentOrderSnapshotResponse> GetPaymentOrderSnapshotAsync(long documentBaseId);
    }
}
