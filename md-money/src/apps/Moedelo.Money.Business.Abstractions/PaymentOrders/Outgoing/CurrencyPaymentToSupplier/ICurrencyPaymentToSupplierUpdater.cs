using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public interface ICurrencyPaymentToSupplierUpdater : IPaymentOrderUpdater<CurrencyPaymentToSupplierSaveRequest, PaymentOrderSaveResponse>
    {
    }
}