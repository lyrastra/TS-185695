using Moedelo.Money.PaymentOrders.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions
{
    public interface IRentPaymentPeriodDao
    {
        Task<IReadOnlyList<RentPaymentPeriod>> GetAsync(int firmId, IReadOnlyCollection<long> ids);
    }
}
