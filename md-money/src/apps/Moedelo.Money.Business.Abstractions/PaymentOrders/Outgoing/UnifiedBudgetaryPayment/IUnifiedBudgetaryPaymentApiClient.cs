using System.Collections.Generic;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using System.Threading.Tasks;
using System.Threading;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentApiClient
    {
        Task<UnifiedBudgetaryPaymentResponse> GetAsync(long documentBaseId);

        Task CreateAsync(UnifiedBudgetaryPaymentSaveRequest request);

        Task<UnifiedBudgetaryPaymentSaveResponse> UpdateAsync(UnifiedBudgetaryPaymentSaveRequest request);

        Task<UnifiedBudgetaryPaymentDeleteResponse> DeleteAsync(long documentBaseId);

        Task<long> GetSubPaymentParentIdAsync(long documentBaseId);

        Task UpdateTaxPostingTypeAsync(long documentBaseId, ProvidePostingType taxPostingType);

        Task<UnifiedBudgetarySubPayment[]> GetSubPaymentsByParentIdsAsync(IReadOnlyCollection<long> documentBaseIds);

        /// <summary>
        /// Возвращает дочерние ЕНП платежи по списку идентификаторов
        /// </summary>
        Task<UnifiedBudgetarySubPayment[]> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds, CancellationToken cancellationToken);
    }
}
