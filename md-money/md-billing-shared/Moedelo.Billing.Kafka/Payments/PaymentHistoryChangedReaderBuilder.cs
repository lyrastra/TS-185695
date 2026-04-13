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
    [InjectAsSingleton(typeof(IPaymentHistoryChangedReaderBuilder))]
    public class PaymentHistoryChangedReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentHistoryChangedReaderBuilder
    {
        public PaymentHistoryChangedReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                BillingTopics.Event.PaymentHistoryCud.EventTopic,
                BillingTopics.Event.PaymentHistoryCud.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IPaymentHistoryChangedReaderBuilder OnPaymentChange(Func<PaymentHistoryChangeEventData, Task> handler)
        {
            OnEvent(handler);

            return this;
        }
    }
}