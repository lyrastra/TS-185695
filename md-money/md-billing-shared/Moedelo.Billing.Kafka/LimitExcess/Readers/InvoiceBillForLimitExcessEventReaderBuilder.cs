using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.LimitExcess.Events;
using Moedelo.Billing.Kafka.Abstractions.LimitExcess.Readers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.LimitExcess.Readers
{
    [InjectAsSingleton(typeof(IInvoiceBillForLimitExcessEventReaderBuilder))]
    internal class InvoiceBillForLimitExcessEventReaderBuilder
        : MoedeloEntityEventKafkaTopicReaderBuilder, IInvoiceBillForLimitExcessEventReaderBuilder
    {
        public InvoiceBillForLimitExcessEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer,
            ILogger<IInvoiceBillForLimitExcessEventReaderBuilder> logger)
            : base(
                BillingTopics.LimitExcess.FirmLimitExcess.EventTopic,
                BillingTopics.LimitExcess.FirmLimitExcess.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer,
                logger)
        {
        }

        public IInvoiceBillForLimitExcessEventReaderBuilder OnBillInvoiced(Func<BillInvoiced, Task> onEvent)
        {
            OnEvent<BillInvoiced>(message =>
            {
                logger.LogInformationExtraData(
                    message,
                    $"Обработка события {nameof(BillInvoiced)}");

                return onEvent(message);
            });

            return this;
        }

        public IInvoiceBillForLimitExcessEventReaderBuilder OnBillInvoicingFailed(Func<BillInvoicingFailed, Task> onEvent)
        {
            OnEvent<BillInvoicingFailed>(message =>
            {
                logger.LogInformationExtraData(
                    message,
                    $"Обработка события {nameof(BillInvoicingFailed)}");

                return onEvent(message);
            });

            return this;
        }
    }
}