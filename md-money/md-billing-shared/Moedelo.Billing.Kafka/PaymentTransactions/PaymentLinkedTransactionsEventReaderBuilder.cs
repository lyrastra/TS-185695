using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.PaymentTransactions;
using Moedelo.Billing.Kafka.Abstractions.PaymentTransactions.Events;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.PaymentTransactions
{
    [InjectAsSingleton(typeof(IPaymentLinkedTransactionsEventReaderBuilder))]
    internal class PaymentLinkedTransactionsEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentLinkedTransactionsEventReaderBuilder
    {
        public PaymentLinkedTransactionsEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                BillingTopics.PaymentTransactions.PaymentLinkedTransactions.Event.Topic,
                BillingTopics.PaymentTransactions.PaymentLinkedTransactions.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IPaymentLinkedTransactionsEventReaderBuilder OnLinkToPaymentChange(Func<PaymentTransactionLinkToPaymentChangeEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}
