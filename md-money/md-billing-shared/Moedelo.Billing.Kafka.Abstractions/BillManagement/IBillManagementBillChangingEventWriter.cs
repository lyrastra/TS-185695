using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.BillManagement.Events;

namespace Moedelo.Billing.Kafka.Abstractions.BillManagement;

/// <summary>
/// Сервис для публикации событий о состоянии запроса на смену параметров счёта
/// </summary>
public interface IBillManagementBillChangingEventWriter
{
    /// <summary>
    /// Публикация события
    /// </summary>
    /// <param name="eventData">Модель данных</param>
    Task WriteAsync(BillChangingStateChangedEvent eventData);
}
