using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.Abstractions;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.FirmAccess;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Accounts.Kafka.NetCore.Readers
{
    [InjectAsSingleton(typeof(IFirmAccessEventReaderBuilder))]
    public sealed class FirmAccessEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IFirmAccessEventReaderBuilder
    {
        public FirmAccessEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                Topics.FirmAccess.Event.Topic,
                Topics.FirmAccess.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IFirmAccessEventReaderBuilder OnFirmAccessChangedEvent(Func<FirmAccessChangedEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IFirmAccessEventReaderBuilder OnFirmAccessChangedEvent(Func<FirmAccessChangedEvent, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IFirmAccessEventReaderBuilder OnFirmAccessGrantedEvent(Func<FirmAccessGrantedEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IFirmAccessEventReaderBuilder OnFirmAccessGrantedEvent(Func<FirmAccessGrantedEvent, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IFirmAccessEventReaderBuilder OnFirmAccessRevokedEvent(Func<FirmAccessRevokedEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IFirmAccessEventReaderBuilder OnFirmAccessRevokedEvent(Func<FirmAccessRevokedEvent, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}