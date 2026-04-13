using System;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions
{
    public interface IPaymentOrderImportedApprover
    {
        Task ApproveAsync(int? settlementAccountId, DateTime? skipline);
    }
}