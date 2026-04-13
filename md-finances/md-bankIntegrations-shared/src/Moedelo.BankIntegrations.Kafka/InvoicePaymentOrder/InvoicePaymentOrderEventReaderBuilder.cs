using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Kafka.Abstractions.InvoicePaymentOrder;
using Moedelo.BankIntegrations.Kafka.Abstractions.InvoicePaymentOrder.Events;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.BankIntegrations.Kafka.InvoicePaymentOrder
{
    [InjectAsSingleton]
    internal sealed class InvoicePaymentOrderEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IInvoicePaymentOrderEventReaderBuilder
    {
        public InvoicePaymentOrderEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  Topics.BankIntegrations.Changed.Event.Topic,
                  Topics.BankIntegrations.Changed.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IInvoicePaymentOrderEventReaderBuilder OnChanged(Func<InvoicePaymentOrderChangedEventData, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}