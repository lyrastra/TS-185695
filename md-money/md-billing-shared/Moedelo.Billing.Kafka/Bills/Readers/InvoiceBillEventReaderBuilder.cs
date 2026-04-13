using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.Bills.Events;
using Moedelo.Billing.Kafka.Abstractions.Bills.Readers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.Bills.Readers
{
    [InjectAsSingleton(typeof(IInvoiceBillEventReaderBuilder))]
    internal class InvoiceBillEventReaderBuilder
        : MoedeloEntityEventKafkaTopicReaderBuilder, IInvoiceBillEventReaderBuilder
    {
        public InvoiceBillEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer,
            ILogger<IInvoiceBillEventReaderBuilder> logger)
            : base(
                BillingTopics.Bills.Bill.EventTopic,
                BillingTopics.Bills.Bill.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer,
                logger)
        {}

        public IInvoiceBillEventReaderBuilder OnBillInvoiced(Func<BillInvoiced, Task> onEvent)
        {
            OnEvent<BillInvoiced>(message =>
            {
                logger.LogInformationExtraData(
                    message,
                    $"Обработка события {nameof(BillInvoiced)}");

                return onEvent(message);
            }, x => x.WithoutContext());

            return this;
        }

        public IInvoiceBillEventReaderBuilder OnBillInvoicingFailed(Func<BillInvoicingFailed, Task> onEvent)
        {
            OnEvent<BillInvoicingFailed>(message =>
            {
                logger.LogInformationExtraData(
                    message,
                    $"Обработка события {nameof(BillInvoicingFailed)}");

                return onEvent(message);
            }, x => x.WithoutContext());

            return this;
        }
    }
}