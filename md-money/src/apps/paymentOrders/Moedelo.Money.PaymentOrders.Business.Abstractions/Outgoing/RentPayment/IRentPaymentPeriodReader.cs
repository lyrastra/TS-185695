using Moedelo.Money.PaymentOrders.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.RentPayment
{
    public interface IRentPaymentPeriodReader
    {
        Task<IReadOnlyList<RentPaymentPeriod>> GetByPaymentBaseIdsAsync(IReadOnlyCollection<long> ids);
    }
}
