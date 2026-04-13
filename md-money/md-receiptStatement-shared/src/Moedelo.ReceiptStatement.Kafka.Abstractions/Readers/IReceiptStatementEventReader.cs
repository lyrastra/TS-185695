using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.ReceiptStatement.Kafka.Abstractions.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.ReceiptStatement.Kafka.Abstractions.Readers
{
    public interface IReceiptStatementEventReader
    {
        /// <summary>
        /// Читает события от документа Акт приема-передачи
        /// </summary>
        /// <param name="groupId">Группа consumer-ов (читают 1 очередь при этом 1 каждое событие обрабатывается только 1 из них) обычно это приложение</param>
        /// <param name="onCreate">Обработчик события создания п/п</param>
        /// <param name="onUpdate">Обработчик события обновления п/п</param>
        /// <param name="onDelete">Обработчик события удаления п/п</param>
        /// <param name="onException">Обработчик исключений</param>
        /// <param name="resetType">При 1 подключении читаем либо с начала очереди, либо только новые сообщения</param>
        /// <param name="consumerCount">Количество потоков чтения (1 поток на 1 partiton)</param>
        Task ReadAsync(string groupId,
            Func<ReceiptStatementCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<ReceiptStatementUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<ReceiptStatementDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
            Func<CUDEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null);
    }
}
