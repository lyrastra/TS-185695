using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanReturn.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanReturn
{
    [InjectAsSingleton(typeof(ILoanReturnEventReaderBuilder))]
    public class LoanReturnEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ILoanReturnEventReaderBuilder
    {
        public LoanReturnEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.LoanReturn.Event.Topic,
                  MoneyTopics.PaymentOrders.LoanReturn.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ILoanReturnEventReaderBuilder OnCreated(Func<LoanReturnCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ILoanReturnEventReaderBuilder OnUpdated(Func<LoanReturnUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ILoanReturnEventReaderBuilder OnDeleted(Func<LoanReturnDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ILoanReturnEventReaderBuilder OnProvideRequired(Func<LoanReturnProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}