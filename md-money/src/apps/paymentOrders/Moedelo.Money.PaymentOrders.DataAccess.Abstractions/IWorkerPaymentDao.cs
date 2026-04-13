using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions
{
    public interface IWorkerPaymentDao
    {
        Task<IReadOnlyList<WorkerPayment>> GetByBaseIdAsync(int firmId, long documentBaseId);

        Task<IReadOnlyDictionary<long, WorkerPayment[]>> GetByBaseIdsAsync(
            int firmId, IReadOnlyCollection<long> documentBaseIds);

        Task InsertAsync(int firmId, long documentBaseId, IReadOnlyCollection<WorkerPayment> payments);

        Task OverwriteAsync(int firmId, long documentBaseId, IReadOnlyCollection<WorkerPayment> payments);

        Task DeleteAsync(int firmId, long documentBaseId);
    }
}
