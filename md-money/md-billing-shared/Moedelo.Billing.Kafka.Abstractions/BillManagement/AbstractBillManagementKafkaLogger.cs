using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Kafka.Abstractions.BillManagement.Events;
using Moedelo.Common.Logging.ExtraLog.ExtraData;

namespace Moedelo.Billing.Kafka.Abstractions.BillManagement;

public abstract class AbstractBillManagementKafkaLogger(
    IBillManagementBillChangingEventWriter writer,
    ILogger logger)
{
    internal protected async Task WriteWrappedAsync(BillChangingStateChangedEvent eventData)
    {
        const string errorMessage = $"Ошибка при публикации {nameof(BillChangingStateChangedEvent)}";
        const string message = "Обновлено состояние запроса на изменение счёта";

        try
        {
            await writer.WriteAsync(eventData);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, errorMessage);
            logger.LogInformationExtraData(eventData, message);
        }
    }
}
