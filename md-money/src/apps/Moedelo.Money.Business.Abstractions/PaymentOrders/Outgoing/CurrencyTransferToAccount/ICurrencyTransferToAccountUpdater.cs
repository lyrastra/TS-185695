using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    public interface ICurrencyTransferToAccountUpdater : IPaymentOrderUpdater<CurrencyTransferToAccountSaveRequest, PaymentOrderSaveResponse>
    {
    }
}