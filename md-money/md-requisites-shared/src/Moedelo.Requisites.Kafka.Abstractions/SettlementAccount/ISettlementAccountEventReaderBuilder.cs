using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Requisites.Kafka.Abstractions.SettlementAccount.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.SettlementAccount
{
    public interface ISettlementAccountEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ISettlementAccountEventReaderBuilder OnCreated(Func<SettlementAccountCreated, Task> onEvent);
        ISettlementAccountEventReaderBuilder OnCreated(Func<SettlementAccountCreated, KafkaMessageValueMetadata, Task> onEvent);
        ISettlementAccountEventReaderBuilder OnArchived(Func<SettlementAccountArchived, Task> onEvent);
        ISettlementAccountEventReaderBuilder OnArchived(Func<SettlementAccountArchived, KafkaMessageValueMetadata, Task> onEvent);
        ISettlementAccountEventReaderBuilder OnDearchived(Func<SettlementAccountDearchived, Task> onEvent);
        ISettlementAccountEventReaderBuilder OnDearchived(Func<SettlementAccountDearchived, KafkaMessageValueMetadata, Task> onEvent);
        ISettlementAccountEventReaderBuilder OnChangedForFirm(Func<SettlementAccountForFirmChanged, Task> onEvent);
    }
}