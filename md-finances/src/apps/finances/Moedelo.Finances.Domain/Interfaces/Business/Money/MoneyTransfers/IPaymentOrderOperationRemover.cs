using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders
{
    public interface IMoneyTransfersOperationRemover
    {
        Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}
