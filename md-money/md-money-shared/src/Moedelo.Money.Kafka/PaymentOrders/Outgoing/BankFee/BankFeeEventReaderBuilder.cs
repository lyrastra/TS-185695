using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BankFee.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.BankFee
{
    [InjectAsSingleton(typeof(IBankFeeEventReaderBuilder))]
    public class BankFeeEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IBankFeeEventReaderBuilder
    {
        public BankFeeEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.BankFee.Event.Topic,
                  MoneyTopics.PaymentOrders.BankFee.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IBankFeeEventReaderBuilder OnCreated(Func<BankFeeCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IBankFeeEventReaderBuilder OnUpdated(Func<BankFeeUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IBankFeeEventReaderBuilder OnDeleted(Func<BankFeeDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IBankFeeEventReaderBuilder OnProvideRequired(Func<BankFeeProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}