using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(PaymentToSupplierSaveRequest saveRequest);
    }
}