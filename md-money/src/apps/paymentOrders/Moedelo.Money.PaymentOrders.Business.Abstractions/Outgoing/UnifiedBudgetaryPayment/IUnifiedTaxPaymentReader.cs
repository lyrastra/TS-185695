using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedTaxPaymentReader
    {
        Task<UnifiedBudgetarySubPayment> GetByBaseIdAsync(long documentBaseId);

        /// <summary>
        /// Возвращает дочерние ЕНП платежи по списку идентификаторов
        /// </summary>
        Task<IReadOnlyCollection<UnifiedBudgetarySubPayment>> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<UnifiedBudgetarySubPayment>> GetByParentBaseIdsAsync(
            IReadOnlyCollection<long> parentsBaseIds);
    }
}