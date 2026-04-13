using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsReader
    {
        Task<PaymentOrderResponse> GetByBaseIdAsync(long documentBaseId);

        Task<IReadOnlyCollection<PaymentOrderResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds);
    }
}