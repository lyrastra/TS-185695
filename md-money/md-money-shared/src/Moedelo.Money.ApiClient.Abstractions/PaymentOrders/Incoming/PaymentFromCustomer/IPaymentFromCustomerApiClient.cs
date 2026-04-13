using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerApiClient
    {
        Task<PaymentFromCustomerDto> GetByIdAsync(long documentBaseId);

        Task<IReadOnlyCollection<PaymentFromCustomerDto>> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}