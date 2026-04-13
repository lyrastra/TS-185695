using System.Threading.Tasks;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerTaxPostingsGenerator
    {
        Task<ITaxPostingsResponse<ITaxPosting>> GenerateAsync(PaymentFromCustomerTaxPostingsGenerateRequest request);
    }
}
