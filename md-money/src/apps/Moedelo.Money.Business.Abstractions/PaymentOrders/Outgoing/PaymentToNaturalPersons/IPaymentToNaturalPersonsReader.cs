using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsReader : IPaymentOrderReader<PaymentToNaturalPersonsResponse>
    {
        Task<IReadOnlyCollection<PaymentToNaturalPersonsResponse>> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
