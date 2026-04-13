using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Requisites.Kafka.Abstractions.AccountantForHour.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.AccountantForHour
{
    public interface IAccountantForHourEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IAccountantForHourEventReaderBuilder OnAccountingRequest(Func<AccountingRequest, Task> onEvent);

        IAccountantForHourEventReaderBuilder OnAccountingRequest(
            Func<AccountingRequest, KafkaMessageValueMetadata, Task> onEvent);
    }
}