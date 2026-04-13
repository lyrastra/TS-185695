using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Requisites.Kafka.Abstractions.NdsRatePeriods.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.NdsRatePeriods;

public interface INdsRatePeriodsEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
{
    INdsRatePeriodsEventReaderBuilder OnNdsRatePeriodsChanged(Func<NdsRatePeriodsChanged, Task> onEvent);
}