using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierAccPostingsFullGenerator
    {
        Task<AccountingPostingsResponse> GenerateAsync(PaymentToSupplierAccPostingsFullGenerateRequest request);
    }
}
