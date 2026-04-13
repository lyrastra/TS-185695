using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetarySubPaymentReader))]
    class UnifiedBudgetarySubPaymentReader(
        IUnifiedBudgetaryPaymentApiClient apiClient) : IUnifiedBudgetarySubPaymentReader
    {
        public Task<long> GetParentIdByBaseIdAsync(long documentBaseId)
        {
            return apiClient.GetSubPaymentParentIdAsync(documentBaseId);
        }

        public Task<UnifiedBudgetarySubPayment[]> GetByByParentIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            return apiClient.GetSubPaymentsByParentIdsAsync(documentBaseIds);
        }

        public Task<UnifiedBudgetarySubPayment[]> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds, CancellationToken ct)
        {
            return apiClient.GetByBaseIdsAsync(documentBaseIds, ct);
        }
    }
}
