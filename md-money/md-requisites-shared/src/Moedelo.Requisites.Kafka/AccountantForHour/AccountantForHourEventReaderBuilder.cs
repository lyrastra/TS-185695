using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Requisites.Kafka.Abstractions.AccountantForHour;
using Moedelo.Requisites.Kafka.Abstractions.AccountantForHour.Events;

namespace Moedelo.Requisites.Kafka.AccountantForHour
{
    [InjectAsSingleton(typeof(IAccountantForHourEventReaderBuilder))]
    internal sealed class AccountantForHourEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder,
        IAccountantForHourEventReaderBuilder
    {
        public AccountantForHourEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                Topics.AccountantForHourEntity.Event.Topic,
                Topics.AccountantForHourEntity.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IAccountantForHourEventReaderBuilder OnAccountingRequest(Func<AccountingRequest, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IAccountantForHourEventReaderBuilder OnAccountingRequest(
            Func<AccountingRequest, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}