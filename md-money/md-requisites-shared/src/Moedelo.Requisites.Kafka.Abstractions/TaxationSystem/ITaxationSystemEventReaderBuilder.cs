using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Requisites.Kafka.Abstractions.TaxationSystem.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.TaxationSystem
{
    public interface ITaxationSystemEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ITaxationSystemEventReaderBuilder OnTaxationSystemChanged(Func<TaxationSystemChanged, Task> onEvent);
    }
}