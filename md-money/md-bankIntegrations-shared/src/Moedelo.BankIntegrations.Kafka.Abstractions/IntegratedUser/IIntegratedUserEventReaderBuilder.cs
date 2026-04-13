using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Kafka.Abstractions.IntegratedUser.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.BankIntegrations.Kafka.Abstractions.IntegratedUser
{
    public interface IIntegratedUserEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IIntegratedUserEventReaderBuilder OnIntegratedUserChanged(Func<IntegratedUserEventData, Task> onEvent);
    }
}