using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders
{
    /// <summary>
    /// Этот сервис оставлен для совместимости. Не используйте и не расширяйте его.
    /// Все методы для изменения данных в домене денег должны быть реализованы в md-money!
    /// </summary>
    public interface IPaymentOrderOperationRemover : IDI
    {
        Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task DeleteUncategorizedAsync(int firmId, int userId, long? sourceId = null);
    }
}
