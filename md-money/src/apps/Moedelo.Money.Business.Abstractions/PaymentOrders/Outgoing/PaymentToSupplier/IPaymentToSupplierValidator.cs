using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierValidator
    {
        Task ValidateAsync(PaymentToSupplierSaveRequest request);
        Task ValidateAsync(SetReserveRequest saveRequest);
    }
}