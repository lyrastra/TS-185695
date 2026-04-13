using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders
{
    /// <summary>
    /// Этот сервис оставлен для совместимости. Не используйте и не расширяйте его.
    /// Все методы для изменения данных в домене денег должны быть реализованы в md-money!
    /// </summary>
    public interface IPaymentOrderOperationUpdater : IDI
    {
        Task ApproveImportedAsync(int firmId, int userId, MoneySourceType sourceType = MoneySourceType.All, long? sourceId = null);
    }
}
