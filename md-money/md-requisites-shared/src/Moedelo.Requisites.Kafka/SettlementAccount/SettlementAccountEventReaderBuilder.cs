using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Requisites.Kafka.Abstractions.SettlementAccount;
using Moedelo.Requisites.Kafka.Abstractions.SettlementAccount.Events;
using Topics = Moedelo.Requisites.Kafka.Abstractions.SettlementAccount.Topics;

namespace Moedelo.Requisites.Kafka.SettlementAccount
{
    [InjectAsSingleton]
    internal sealed class SettlementAccountEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ISettlementAccountEventReaderBuilder
    {
        public SettlementAccountEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                Topics.SettlementAccountEntity.Event.Topic, 
                Topics.SettlementAccountEntity.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public ISettlementAccountEventReaderBuilder OnCreated(Func<SettlementAccountCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
        
        public ISettlementAccountEventReaderBuilder OnCreated(Func<SettlementAccountCreated, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
        
        public ISettlementAccountEventReaderBuilder OnArchived(Func<SettlementAccountArchived, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
        
        public ISettlementAccountEventReaderBuilder OnArchived(Func<SettlementAccountArchived, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
        
        public ISettlementAccountEventReaderBuilder OnDearchived(Func<SettlementAccountDearchived, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
        
        public ISettlementAccountEventReaderBuilder OnDearchived(Func<SettlementAccountDearchived, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ISettlementAccountEventReaderBuilder OnChangedForFirm(Func<SettlementAccountForFirmChanged, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}