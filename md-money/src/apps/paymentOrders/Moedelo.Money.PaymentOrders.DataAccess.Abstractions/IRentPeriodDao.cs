using Moedelo.Money.PaymentOrders.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions
{
    public interface IRentPeriodDao
    {
        Task<IReadOnlyList<RentPeriod>> GetAsync(int firmId, long documentBaseId);

        Task InsertAsync(int firmId, long documentBaseId, IReadOnlyCollection<RentPeriod> periods);

        Task OverwriteAsync(int firmId, long documentBaseId, IReadOnlyCollection<RentPeriod> periods);

        Task DeleteAsync(int firmId, long documentBaseId);
    }
}
