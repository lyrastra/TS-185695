using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentUpdater : IPaymentOrderUpdater<BudgetaryPaymentSaveRequest, PaymentOrderSaveResponse>
    {
    }
}