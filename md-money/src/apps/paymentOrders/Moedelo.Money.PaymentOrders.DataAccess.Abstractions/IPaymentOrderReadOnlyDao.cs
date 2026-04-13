using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions
{
    public interface IPaymentOrderReadOnlyDao
    {
        Task<IReadOnlyList<int>> GetOutgoingNumbersAsync(int firmId, int settlementAccountId, int? year, int? cut);

        Task<bool> GetIsPaidAsync(int firmId, long documentBaseId);
        
        Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusAsync(DocumentsStatusRequest request);
    }
}
