using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment;

namespace Moedelo.Money.Client.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment.SubPayment
{
    public interface IUnifiedBudgetarySubPaymentClient: IDI
    {
        /// <summary>
        /// Возвращает дочерние ЕНП платежи по списку идентификаторов
        /// </summary>
        Task<UnifiedBudgetarySubPaymentResponseDto[]> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds, CancellationToken cancellationToken);
        Task<UnifiedBudgetarySubPaymentResponseDto[]> GetByParentIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);
    }
}