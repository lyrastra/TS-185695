using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Events;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public interface ICurrencyPaymentToSupplierEventReader
    {
        /// <summary>
        /// Читает события от п/п "Оплата поставщику в валюте"
        /// </summary>
        /// <param name="groupId">Группа consumer-ов (читают 1 очередь при этом 1 каждое событие обрабатывается только 1 из них) обычно это приложение</param>
        /// <param name="onCreate">Обработчик события создания п/п</param>
        /// <param name="onUpdate">Обработчик события обновления п/п</param>
        /// <param name="onDelete">Обработчик события удаления п/п</param>
        /// <param name="onException">Обработчик исключений</param>
        /// <param name="onFatalException">Обработчик фатальных исключений</param>
        /// <param name="resetType">При 1 подключении читаем либо с начала очереди, либо только новые сообщения</param>
        /// <param name="consumerCount">Количество потоков чтения (1 поток на 1 partiton)</param>
        /// <param name="retrySettings"></param>
        /// <param name="cancellationToken"></param>
        Task ReadAsync(string groupId,
            Func<CurrencyPaymentToSupplierCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<CurrencyPaymentToSupplierUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<CurrencyPaymentToSupplierDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
            Func<CUDEventMessageValue, Exception, Task> onException = null,
            Func<Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            ConsumerActionRetrySettings retrySettings = null,
            CancellationToken? cancellationToken = null);
    }
}