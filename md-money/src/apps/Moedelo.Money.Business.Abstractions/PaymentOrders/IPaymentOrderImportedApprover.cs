using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface IPaymentOrderImportedApprover
    {
        Task ApproveAsync(int? settlementAccountId, DateTime? skipline);
    }
}