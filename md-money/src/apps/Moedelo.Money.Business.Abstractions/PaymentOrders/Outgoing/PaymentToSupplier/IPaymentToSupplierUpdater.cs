using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierUpdater : IPaymentOrderUpdater<PaymentToSupplierSaveRequest, PaymentOrderSaveResponse>
    {
        Task SetReserveAsync(SetReserveRequest request);
    }
}