using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerValidator
    {
        Task ValidateAsync(PaymentFromCustomerSaveRequest request);
        Task ValidateAsync(SetReserveRequest request);
    }
}