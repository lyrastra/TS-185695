using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierIgnoreNumberSaver
    {
        Task ApplyIgnoreNumberAsync(PaymentToSupplierApplyIgnoreNumberRequest applyRequest);
    }
}