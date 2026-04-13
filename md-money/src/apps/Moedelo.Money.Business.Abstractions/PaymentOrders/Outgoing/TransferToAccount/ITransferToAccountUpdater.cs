using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount
{
    public interface ITransferToAccountUpdater : IPaymentOrderUpdater<TransferToAccountSaveRequest, PaymentOrderSaveResponse>
    {
    }

    public interface ITransferToAccountUpdaterExt : IPaymentOrderUpdater<TransferToAccountSaveRequest, TransferToAccountSaveResponse>
    {
    }
}