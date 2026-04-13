using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetarySubPaymentReader
    {
        Task<long> GetParentIdByBaseIdAsync(long documentBaseId);

        Task<UnifiedBudgetarySubPayment[]> GetByByParentIdsAsync(IReadOnlyCollection<long> documentBaseIds);

        /// <summary>
        /// Возвращает дочерние ЕНП платежи по списку идентификаторов
        /// </summary>
        Task<UnifiedBudgetarySubPayment[]> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds, CancellationToken cancellationToken);
    }
}
