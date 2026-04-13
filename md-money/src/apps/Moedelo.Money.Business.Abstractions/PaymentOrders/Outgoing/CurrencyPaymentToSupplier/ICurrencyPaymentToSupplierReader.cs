using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public interface ICurrencyPaymentToSupplierReader : IPaymentOrderReader<CurrencyPaymentToSupplierResponse>
    {
    }
}