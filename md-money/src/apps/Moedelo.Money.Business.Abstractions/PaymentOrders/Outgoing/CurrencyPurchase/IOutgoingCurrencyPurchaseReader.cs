using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase
{
    public interface IOutgoingCurrencyPurchaseReader : IPaymentOrderReader<OutgoingCurrencyPurchaseResponse>
    {
    }
}