using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Kafka.Abstractions.Sales.Bills.Events;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Bills
{
    public interface ISalesBillPaymentUpdatedReader
    {
        /// <summary>
        /// Читает события от Счетов в продажах
        /// </summary>
        /// <param name="groupId">Группа consumer-ов (читают 1 очередь при этом 1 каждое событие обрабатывается только 1 из них) обычно это приложение</param>
        /// <param name="onPaymentUpdate">Обработчик события</param>
        /// <param name="onException">Обработчик исключений</param>
        /// <param name="resetType">При 1 подключении читаем либо с начала очереди, либо только новые сообщения</param>
        /// <param name="consumerCount">Количество потоков чтения (1 поток на 1 partiton)</param>
        Task ReadAsync(
            string groupId,
            Func<SalesBillPaymentMessage, KafkaMessageValueMetadata, Task> onPaymentUpdate,
            Func<DocsCudEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null);
    }
}
