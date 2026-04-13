using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.Abstractions
{
    public interface ICashOrderReader
    {
        Task<CashOrderResponse> GetByBaseIdAsync(long documentBaseId, OperationType operationType);
        Task<OperationType> GetOperationTypeAsync(long documentBaseId);
        Task<IReadOnlyList<OperationTypeResponse>> GetOperationTypeByIdsAsync(IReadOnlyCollection<long> documentBaseIds);
        Task<long> GetOperationIdAsync(long documentBaseId);
        Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}