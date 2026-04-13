using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerReader
    {
        Task<PaymentFromCustomerResponse> GetByBaseIdAsync(long baseId);
        Task<IReadOnlyCollection<PaymentFromCustomerResponse>> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
