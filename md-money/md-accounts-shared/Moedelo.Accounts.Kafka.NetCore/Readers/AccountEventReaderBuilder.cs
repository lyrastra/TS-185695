using Moedelo.Accounts.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Accounts;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

namespace Moedelo.Accounts.Kafka.NetCore.Readers
{
    [InjectAsSingleton(typeof(IAccountEventReaderBuilder))]
    public sealed class AccountEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IAccountEventReaderBuilder
    {
        public AccountEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                Topics.Account.Event.Topic,
                Topics.Account.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IAccountEventReaderBuilder OnAccountWasMergedWithAnotherAccountEvent(
            Func<AccountWasMergedWithAnotherAccountEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IAccountEventReaderBuilder OnFirmWasRemovedFromAccountEvent(Func<FirmWasRemovedFromAccountEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IAccountEventReaderBuilder OnNewFirmWasCreatedInAccountEvent(Func<NewFirmWasCreatedInAccountEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IAccountEventReaderBuilder OnNewUserWasCreatedInAccountEvent(Func<NewUserWasCreatedInAccountEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}