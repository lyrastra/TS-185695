using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Requisites.Kafka.Abstractions.Calendar.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Calendar
{
    /// <summary>
    /// Ридер для справочника (налогового) календаря
    /// </summary>
    public interface ICommonCalendarEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ICommonCalendarEventReaderBuilder OnChanged(Func<CommonCalendarEventChanged, Task> onEvent);
    }
}
