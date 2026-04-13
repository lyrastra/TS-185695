using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices.Events;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices
{
    public interface ISalesCurrencyInvoiceEventReader
    {
        /// <summary>
        /// Читает события от документа Исходящий инвойс
        /// </summary>
        /// <param name="groupId">Группа consumer-ов (читают 1 очередь при этом 1 каждое событие обрабатывается только 1 из них) обычно это приложение</param>
        /// <param name="onCreate">Обработчик события создания п/п</param>
        /// <param name="onUpdate">Обработчик события обновления п/п</param>
        /// <param name="onDelete">Обработчик события удаления п/п</param>
        /// <param name="onException">Обработчик исключений</param>
        /// <param name="resetType">При 1 подключении читаем либо с начала очереди, либо только новые сообщения</param>
        /// <param name="consumerCount">Количество потоков чтения (1 поток на 1 partiton)</param>
        Task ReadAsync(string groupId,
            Func<SalesCurrencyInvoiceCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<SalesCurrencyInvoiceUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<SalesCurrencyInvoiceDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
            Func<DocsCudEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null);
    }
}