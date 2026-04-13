using System.Threading.Tasks;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerAccPostingsFullGenerator
    {
        Task<AccountingPostingsResponse> GenerateAsync(PaymentFromCustomerAccPostingsFullGenerateRequest request);
    }
}
