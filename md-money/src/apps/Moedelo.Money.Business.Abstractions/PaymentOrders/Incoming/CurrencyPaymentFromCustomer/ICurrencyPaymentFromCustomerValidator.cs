using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    public interface ICurrencyPaymentFromCustomerValidator
    {
        Task ValidateAsync(CurrencyPaymentFromCustomerSaveRequest request);
    }
}