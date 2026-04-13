using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierTaxPostingsGenerator
    {
        Task<ITaxPostingsResponse<ITaxPosting>> GenerateAsync(PaymentToSupplierTaxPostingsGenerateRequest request);
    }
}
