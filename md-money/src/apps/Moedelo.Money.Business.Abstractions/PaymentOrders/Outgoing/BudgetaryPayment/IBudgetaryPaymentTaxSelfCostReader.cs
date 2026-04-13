using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Domain.SelfCostPayments;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentTaxSelfCostReader
    {
        Task<IReadOnlyCollection<SelfCostPayment>> GetSelfCostPaymentsAsync(SelfCostPaymentRequest request);
    }
}