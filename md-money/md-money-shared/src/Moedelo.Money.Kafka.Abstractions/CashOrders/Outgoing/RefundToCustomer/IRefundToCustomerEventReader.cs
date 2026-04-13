using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.RefundToCustomer.Events;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.RefundToCustomer
{
    public interface IRefundToCustomerEventReader
    {
        /// <summary>
        /// Читает события от РКО "Возврат покупателю"
        /// </summary>
        /// <param name="groupId">Группа consumer-ов (читают 1 очередь при этом 1 каждое событие обрабатывается только 1 из них) обычно это приложение</param>
        /// <param name="onCreate">Обработчик события создания РКО</param>
        /// <param name="onUpdate">Обработчик события обновления РКО</param>
        /// <param name="onDelete">Обработчик события удаления РКО</param>
        /// <param name="onException">Обработчик исключений</param>
        /// <param name="onFatalException">Обработчик фатальных исключений</param>
        /// <param name="resetType">При 1 подключении читаем либо с начала очереди, либо только новые сообщения</param>
        /// <param name="consumerCount">Количество потоков чтения (1 поток на 1 partiton)</param>
        /// <param name="cancellationToken"></param>
        Task ReadAsync(string groupId,
            Func<RefundToCustomerCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<RefundToCustomerUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<RefundToCustomerDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
            Func<CUDEventMessageValue, Exception, Task> onException = null,
            Func<Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType =
                KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null);
    }
}