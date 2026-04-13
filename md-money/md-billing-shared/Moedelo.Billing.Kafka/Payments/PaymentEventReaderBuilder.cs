using System;
using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.Payments;
using Moedelo.Billing.Kafka.Abstractions.Payments.Events;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.Payments
{
    [InjectAsSingleton(typeof(IPaymentEventReaderBuilder))]
    public class PaymentEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentEventReaderBuilder
    {
        public PaymentEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                BillingTopics.Event.PaymentChanged.EventTopic,
                BillingTopics.Event.PaymentChanged.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IPaymentEventReaderBuilder OnPaymentSuccessSwitch(Func<PaymentEventData, Task> handler)
        {
            OnEvent(handler);

            return this;
        }
    }
}