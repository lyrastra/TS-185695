using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;

namespace Moedelo.Money.Kafka.PaymentOrders.Common
{
    [InjectAsSingleton(typeof(IPaymentOrdersOperationEventReaderBuilder))]
    public class PaymentOrdersOperationEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentOrdersOperationEventReaderBuilder
    {
        public PaymentOrdersOperationEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.Operation.Event.Topic,
                  MoneyTopics.PaymentOrders.Operation.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPaymentOrdersOperationEventReaderBuilder OnTypeChanged(Func<OperationTypeChanged, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}