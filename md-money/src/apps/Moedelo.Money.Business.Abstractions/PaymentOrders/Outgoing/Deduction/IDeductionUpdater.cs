using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction
{
    public interface IDeductionUpdater : IPaymentOrderUpdater<DeductionSaveRequest, PaymentOrderSaveResponse>
    {
    }
}