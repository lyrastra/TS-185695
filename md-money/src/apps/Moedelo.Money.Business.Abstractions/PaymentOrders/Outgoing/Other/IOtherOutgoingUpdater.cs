using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other
{
    public interface IOtherOutgoingUpdater : IPaymentOrderUpdater<OtherOutgoingSaveRequest, PaymentOrderSaveResponse>
    {
    }
}