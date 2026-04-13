using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Firm;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers
{
    public interface IFirmEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IFirmEventReaderBuilder OnFirmIsInternalChangedEvent(Func<FirmIsInternalChangedEvent, Task> onEvent);
        
        IFirmEventReaderBuilder OnFirmMarkedIsDeletedEvent(Func<FirmMarkedIsDeletedEvent, Task> onEvent);
    }
}