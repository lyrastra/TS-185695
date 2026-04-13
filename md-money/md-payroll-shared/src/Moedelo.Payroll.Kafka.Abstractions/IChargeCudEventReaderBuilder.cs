using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Payroll.Kafka.Abstractions.Events;

namespace Moedelo.Payroll.Kafka.Abstractions
{
    public interface IChargeCudEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IChargeCudEventReaderBuilder OnChargeCudEvent(Func<ChargeCudEventMessage, Task> onEvent);
    }
}