using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierTaxPostingsApiClient
    {
        Task<string> GenerateAsync(PaymentToSupplierTaxPostingsGenerateRequestDto generateRequest);
    }
}
