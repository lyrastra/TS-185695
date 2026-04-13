using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Konragents.Kafka.Abstractions.Kontragent.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Konragents.Kafka.Abstractions.Kontragent
{
    /// <summary>
    /// Контрагент. Чтение событий
    /// </summary>
    public interface IKontragentEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IKontragentEventReaderBuilder OnCreated(Func<KontragentCreated, Task> onEvent);
        IKontragentEventReaderBuilder OnCreated(Func<KontragentCreated, KafkaMessageValueMetadata, Task> onEvent);

        IKontragentEventReaderBuilder OnUpdated(Func<KontragentUpdated, Task> onEvent);
        IKontragentEventReaderBuilder OnUpdated(Func<KontragentUpdated, KafkaMessageValueMetadata, Task> onEvent);

        IKontragentEventReaderBuilder OnArchived(Func<KontragentArchived, Task> onEvent);
        IKontragentEventReaderBuilder OnArchived(Func<KontragentArchived, KafkaMessageValueMetadata, Task> onEvent);

        IKontragentEventReaderBuilder OnDearchived(Func<KontragentDearchived, Task> onEvent);
        IKontragentEventReaderBuilder OnDearchived(Func<KontragentDearchived, KafkaMessageValueMetadata, Task> onEvent);

        IKontragentEventReaderBuilder OnDeleted(Func<KontragentDeleted, Task> onEvent);
        IKontragentEventReaderBuilder OnDeleted(Func<KontragentDeleted, KafkaMessageValueMetadata, Task> onEvent);
    }
}
