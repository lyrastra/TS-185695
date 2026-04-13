using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Commands.CashOrders
{
    public interface ICashOrderChangeTaxationSystemCommandReader
    {
        /// <summary>
        /// Читает события изменения СНО в кассовом ордере
        /// </summary>
        /// <param name="groupId">Группа consumer-ов (читают 1 очередь при этом 1 каждое событие обрабатывается только 1 из них) обычно это приложение</param>
        /// <param name="onChange">Обработчик события</param>
        /// <param name="onFatalException">Обработчик исключений</param>
        /// <param name="resetType">При 1 подключении читаем либо с начала очереди, либо только новые сообщения</param>
        /// <param name="consumerCount">Количество потоков чтения (1 поток на 1 partiton)</param>
        Task ReadAsync(
            string groupId,
            Func<CashOrderChangeTaxationSystemCommandMessage, KafkaMessageValueMetadata, Task> onChange,
            Func<CashOrderChangeTaxationSystemCommandMessageValue, Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null);
    }
}
