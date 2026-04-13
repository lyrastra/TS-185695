using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.PaymentCategory.Events;
using Moedelo.Billing.Kafka.Abstractions.PaymentCategory.Readers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.PaymentCategory.Readers;

[InjectAsSingleton(typeof(IPaymentCategoryEventReaderBuilder))]
public class PaymentCategoryEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentCategoryEventReaderBuilder
{
    public PaymentCategoryEventReaderBuilder(
        IMoedeloEntityEventKafkaTopicReader reader,
        IExecutionInfoContextInitializer contextInitializer,
        IExecutionInfoContextAccessor contextAccessor,
        IAuditTracer auditTracer,
        ILogger<IPaymentCategoryEventReaderBuilder> logger)
        : base(
            BillingTopics.PaymentHistory.PaymentCategory.Event.EventTopic,
            BillingTopics.PaymentHistory.PaymentCategory.EntityName,
            reader,
            contextInitializer,
            contextAccessor,
            auditTracer,
            logger)
    {}
    
    public IPaymentCategoryEventReaderBuilder OnNotesOnPayments(Func<PaymentCategoryEvent, Task> onEvent)
    {
        OnEvent<PaymentCategoryEvent>(message =>
        {
            logger.LogInformationExtraData(
                message,
                $"Обработка события {nameof(PaymentCategoryEvent)}");

            return onEvent(message);
        }, x => x.WithoutContext());

        return this;
    }
}