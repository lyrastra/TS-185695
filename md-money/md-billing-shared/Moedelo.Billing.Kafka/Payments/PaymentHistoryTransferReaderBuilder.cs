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
    [InjectAsSingleton(typeof(IPaymentHistoryTransferReaderBuilder))]
    public class PaymentHistoryTransferReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentHistoryTransferReaderBuilder
    {
        public PaymentHistoryTransferReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                BillingTopics.Event.PaymentHistoryTransferred.EventTopic,
                BillingTopics.Event.PaymentHistoryTransferred.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IPaymentHistoryTransferReaderBuilder OnPaymentTransferred(Func<PaymentHistoryTransferredEventData, Task> handler)
        {
            OnEvent(handler);

            return this;
        }
    }
}